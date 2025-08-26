using Celestial.Agency.WebClient.Services.ApiClients;
using Celestial.Agency.WebClient.Services.Implementation;
using Celestial.Agency.WebClient.Services.Interface;

namespace Celestial.Agency.WebClient.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebClientServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<TourApiClient>(client => { client.BaseAddress = new Uri(config["ApiBaseUrl"]); });

        services.AddScoped<ITourService, TourService>();

        return services;
    }
}