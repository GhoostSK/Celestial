using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Geo;

public interface IStateService : IRepositoryEF<State>, IScopeLifetime
{
    public Task<List<State>> GetStateList(int countryId, CancellationToken cancellationToken);
}