using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Geo;

public interface ICountryService : IRepositoryEF<Country>, IScopeLifetime
{
    public Task<List<Country>> GetCountryList(CancellationToken cancellationToken);
}