using Celestial.Agency.Core.DAL.Model.Geo;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Geo;

public interface ICountryService : IRepositoryEF<Country>, IScopeLifetime
{
    public Task<List<Country>> GetCountryList(CancellationToken cancellationToken);
}