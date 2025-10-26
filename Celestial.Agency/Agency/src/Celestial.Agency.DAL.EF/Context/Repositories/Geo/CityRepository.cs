using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Geo;

public class CityRepository(MainDbContext dbContext) : RepositoryBase<City>(dbContext), ICityService
{
    public async Task<List<City>> GetCityList(int countryId, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => p.country_id == countryId)
            .ToListAsync(cancellationToken);
    }
}