using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Celestial.WebClient.Services.Authentication;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Base;

/// <summary>
///     لایه‌ی پایه برای تمام فراخوانی‌های HTTP به سمت API.
///     پاسخ موفق را به مدل مقصد نگاشت می‌کند و خطاهای API را در قالب
///     <see cref="ServiceResContextBase" /> برمی‌گرداند تا صفحات بتوانند آن را نمایش دهند.
///     اگر کاربر وارد شده باشد، توکن او به‌صورت Bearer به API ارسال می‌شود.
/// </summary>
public class HttpJsonClient(HttpClient http, IUserSessionService userSession)
{
    /// <summary>
    ///     API مقادیر enum را به‌صورت رشته سریالایز می‌کند (JsonStringEnumConverter در سمت سرور)،
    ///     بنابراین کلاینت نیز باید همان تنظیمات را داشته باشد.
    /// </summary>
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<T> GetAsync<T>(string url) where T : ServiceResContextBase, new()
    {
        return await SendAsync<T>(async () =>
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            await AuthorizeAsync(request);

            return await http.SendAsync(request);
        });
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
        where TResponse : ServiceResContextBase, new()
    {
        return await SendAsync<TResponse>(async () =>
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(data, options: JsonOptions)
            };
            await AuthorizeAsync(request);

            return await http.SendAsync(request);
        });
    }

    /// <summary>
    ///     برای اندپوینت‌هایی که در پاسخ موفق فقط یک رشته برمی‌گردانند
    ///     (مثل ثبت‌نام و تأیید شماره تلفن) و قابل نگاشت به مدل نیستند.
    /// </summary>
    public async Task<ApiMessageResDto> PostForMessageAsync<TRequest>(string url, TRequest data)
    {
        HttpResponseMessage response;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(data, options: JsonOptions)
            };
            await AuthorizeAsync(request);

            response = await http.SendAsync(request);
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            return Failure<ApiMessageResDto>(0, exception.Message);
        }

        if (!response.IsSuccessStatusCode)
            return Failure<ApiMessageResDto>((int)response.StatusCode, await ReadErrorMessageAsync(response));

        return new ApiMessageResDto
        {
            StatusCode = (int)response.StatusCode,
            Message = await ReadMessageAsync(response)
        };
    }

    private async Task AuthorizeAsync(HttpRequestMessage request)
    {
        var token = await userSession.GetAccessTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private static async Task<string> ReadMessageAsync(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(body))
            return string.Empty;

        try
        {
            using var document = JsonDocument.Parse(body);

            return document.RootElement.ValueKind == JsonValueKind.String
                ? document.RootElement.GetString() ?? string.Empty
                : body;
        }
        catch (JsonException)
        {
            return body;
        }
    }

    private static async Task<T> SendAsync<T>(Func<Task<HttpResponseMessage>> send) where T : ServiceResContextBase, new()
    {
        HttpResponseMessage response;

        try
        {
            response = await send();
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            return Failure<T>(0, exception.Message);
        }

        if (!response.IsSuccessStatusCode)
            return Failure<T>((int)response.StatusCode, await ReadErrorMessageAsync(response));

        try
        {
            var result = await response.Content.ReadFromJsonAsync<T>(JsonOptions);

            return result ?? Failure<T>((int)response.StatusCode, "Empty response from server");
        }
        catch (JsonException exception)
        {
            return Failure<T>((int)response.StatusCode, exception.Message);
        }
    }

    /// <summary>
    ///     خطاهای API معمولاً در قالب <c>{ code, message }</c> برگردانده می‌شوند، اما برخی اندپوینت‌ها
    ///     یک رشته‌ی ساده یا خطای اعتبارسنجی ModelState برمی‌گردانند؛ هر سه حالت پشتیبانی می‌شود.
    /// </summary>
    private static async Task<string> ReadErrorMessageAsync(HttpResponseMessage response)
    {
        var fallback = $"Request failed {(int)response.StatusCode}";
        var body = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(body))
            return fallback;

        try
        {
            using var document = JsonDocument.Parse(body);

            return document.RootElement.ValueKind switch
            {
                JsonValueKind.String => document.RootElement.GetString() ?? fallback,
                JsonValueKind.Object => ReadObjectErrorMessage(document.RootElement) ?? fallback,
                _ => body
            };
        }
        catch (JsonException)
        {
            // پاسخ JSON نیست؛ متن خام قابل نمایش است
            return body;
        }
    }

    private static string? ReadObjectErrorMessage(JsonElement root)
    {
        if (root.TryGetProperty("message", out var message) && message.ValueKind == JsonValueKind.String)
            return message.GetString();

        // خطاهای اعتبارسنجی ModelState: { errors: { "Field": ["message", ...] } }
        if (!root.TryGetProperty("errors", out var errors) || errors.ValueKind != JsonValueKind.Object)
            return root.TryGetProperty("title", out var title) ? title.GetString() : null;

        var messages = errors.EnumerateObject()
            .SelectMany(property => property.Value.ValueKind == JsonValueKind.Array
                ? property.Value.EnumerateArray().Select(item => item.GetString())
                : new[] { property.Value.GetString() })
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToList();

        return messages.Any() ? string.Join(" ", messages) : null;
    }

    private static T Failure<T>(int statusCode, string message) where T : ServiceResContextBase, new()
    {
        return new T
        {
            HasError = true,
            StatusCode = statusCode,
            ErrorMessage = message
        };
    }

    private sealed class ApiErrorDto
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
    }
}
