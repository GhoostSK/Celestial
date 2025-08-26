using Celestial.WebClient.Services.ApiClients;
using Celestial.WebClient.Services.Implementation;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebClientServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<TourApiClient>(client => { client.BaseAddress = new Uri(config["ApiBaseUrl"]); });
        services.AddHttpClient<BlogApiClient>(client => { client.BaseAddress = new Uri(config["ApiBaseUrl"]); });

        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IBlogService, BlogService>();

        return services;
    }
}