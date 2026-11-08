using System.Text;

namespace Celestial.WebClient.Services.Extensions;

/// <summary>
///     ساخت امن کوئری‌استرینگ برای فراخوانی API.
///     مقادیر خالی حذف می‌شوند و لیست‌ها به‌صورت تکرار همان کلید ارسال می‌شوند
///     (قالبی که Model Binding در ASP.NET Core انتظار دارد).
/// </summary>
public sealed class ApiQueryBuilder
{
    private readonly StringBuilder _query = new();

    public ApiQueryBuilder Add(string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return this;

        Append(key, value);
        return this;
    }

    public ApiQueryBuilder Add(string key, int? value, bool skipZero = true)
    {
        if (value is null || (skipZero && value == 0))
            return this;

        Append(key, value.Value.ToString());
        return this;
    }

    public ApiQueryBuilder Add(string key, long? value, bool skipZero = true)
    {
        if (value is null || (skipZero && value == 0))
            return this;

        Append(key, value.Value.ToString());
        return this;
    }

    public ApiQueryBuilder Add(string key, double? value, bool skipZero = true)
    {
        if (value is null || (skipZero && value == 0))
            return this;

        Append(key, value.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        return this;
    }

    public ApiQueryBuilder Add(string key, bool? value, bool skipFalse = true)
    {
        if (value is null || (skipFalse && value == false))
            return this;

        Append(key, value.Value ? "true" : "false");
        return this;
    }

    public ApiQueryBuilder AddRange<T>(string key, IEnumerable<T>? values)
    {
        if (values is null)
            return this;

        foreach (var value in values)
        {
            var text = value?.ToString();

            if (!string.IsNullOrWhiteSpace(text))
                Append(key, text);
        }

        return this;
    }

    public override string ToString()
    {
        return _query.Length == 0 ? string.Empty : $"?{_query}";
    }

    private void Append(string key, string value)
    {
        if (_query.Length > 0)
            _query.Append('&');

        _query.Append(Uri.EscapeDataString(key))
            .Append('=')
            .Append(Uri.EscapeDataString(value));
    }
}
