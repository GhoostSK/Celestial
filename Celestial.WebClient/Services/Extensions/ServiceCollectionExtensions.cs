using Celestial.WebClient.Services.ApiClients;
using Celestial.WebClient.Services.Implementation;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebClientServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<BlogApiClient>(client => { client.BaseAddress = new Uri(config["ApiBaseUrl"]); });

        services.AddScoped<IBlogService, BlogService>();

        return services;
    }
}

public class ServiceResContextBase
{
    public bool HasError { get; set; } = false;
    public int StatusCode { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
}