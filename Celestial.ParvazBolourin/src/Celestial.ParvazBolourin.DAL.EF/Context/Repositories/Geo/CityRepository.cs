using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Geo;

public class CityRepository(MainDbContext dbContext) : RepositoryBase<City>(dbContext), ICityService
{
    public async Task<List<City>> GetCityList(int stateId, CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => p.State_id == stateId).ToListAsync(cancellationToken);
    }
}