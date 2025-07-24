using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Geo;

public class CountryRepository(MainDbContext dbContext) : RepositoryBase<Country>(dbContext), ICountryService
{
    public async Task<List<Country>> GetCountryList(CancellationToken cancellationToken)
    {
        var data = await TableNoTracking.ToListAsync(cancellationToken);
        return await TableNoTracking.ToListAsync(cancellationToken);
    }
}