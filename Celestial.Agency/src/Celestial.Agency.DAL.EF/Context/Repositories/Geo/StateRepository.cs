using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Geo;

public class StateRepository(MainDbContext dbContext) : RepositoryBase<State>(dbContext), IStateService
{
    public async Task<List<State>> GetStateList(int countryId, CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => p.Country_id == countryId).ToListAsync(cancellationToken);
    }
}