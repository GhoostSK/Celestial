using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Geo;

public interface ICityService : IRepositoryEF<City>, IScopeLifetime
{
    public Task<List<City>> GetCityList(int countryId, CancellationToken cancellationToken);
}