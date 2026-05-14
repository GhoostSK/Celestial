using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Base;

public class HttpJsonClient(HttpClient http)
{
    public async Task<T> GetAsync<T>(string url) where T : ServiceResContextBase, new()
    {
        var response = await http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return new T
            {
                HasError = true,
                ErrorMessage = $"Request failed {response.StatusCode}",
                StatusCode = (int)response.StatusCode
            };

        var result = await response.Content.ReadFromJsonAsync<T>();
        if (result is null)
            return new T
            {
                HasError = true,
                ErrorMessage = "Empty response from server"
            };

        return result;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
        where TResponse : ServiceResContextBase, new()
    {
        var response = await http.PostAsJsonAsync(url, data);

        if (!response.IsSuccessStatusCode)
            return new TResponse
            {
                HasError = true,
                ErrorMessage = $"Request failed {response.StatusCode}",
                StatusCode = (int)response.StatusCode
            };

        var result = await response.Content.ReadFromJsonAsync<TResponse>();
        if (result is null)
            return new TResponse
            {
                HasError = true,
                ErrorMessage = "Empty response from server"
            };

        return result;
    }
}