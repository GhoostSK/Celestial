using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.DependencyInjection;

public static class DalEfConfiguration
{
    public static IServiceCollection AddDalEf(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        var connectionString = configuration.GetConnectionString("DBContext");
        services.AddConfiguredMsSqlDbContext<MainDbContext>(connectionString, hostEnvironment);

        return services;
    }
}