using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;

public interface ICityService : IRepositoryEF<City>, IScopeLifetime
{
    public Task<List<City>> GetCityList(int countryId, CancellationToken cancellationToken);
}