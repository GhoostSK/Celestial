using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Geo;

public class CountryRepository(MainDbContext dbContext) : RepositoryBase<Country>(dbContext), ICountryService
{
    public async Task<List<Country>> GetCountryList(CancellationToken cancellationToken)
    {
        var data = await TableNoTracking.ToListAsync(cancellationToken);
        return await TableNoTracking.ToListAsync(cancellationToken);
    }
}