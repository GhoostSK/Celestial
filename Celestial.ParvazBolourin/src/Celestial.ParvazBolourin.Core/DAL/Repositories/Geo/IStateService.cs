using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;

public interface IStateService : IRepositoryEF<State>, IScopeLifetime
{
    public Task<List<State>> GetStateList(int countryId, CancellationToken cancellationToken);
}