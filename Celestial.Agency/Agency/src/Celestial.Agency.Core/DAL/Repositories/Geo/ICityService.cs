using Celestial.Agency.Core.DAL.Model.Geo;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Geo;

public interface ICityService : IRepositoryEF<City>, IScopeLifetime
{
    public Task<List<City>> GetCityList(int countryId, CancellationToken cancellationToken);
}