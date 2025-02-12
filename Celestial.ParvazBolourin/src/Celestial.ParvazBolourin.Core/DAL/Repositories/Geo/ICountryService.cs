using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;

public interface ICountryService : IRepositoryEF<Country>, IScopeLifetime
{
    public Task<List<Country>> GetCountryList(CancellationToken cancellationToken);
}