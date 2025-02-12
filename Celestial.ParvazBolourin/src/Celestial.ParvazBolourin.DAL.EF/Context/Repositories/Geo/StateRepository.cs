using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Geo;

public class StateRepository(MainDbContext dbContext) : RepositoryBase<State>(dbContext), IStateService
{
    public async Task<List<State>> GetStateList(int countryId, CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => p.Country_id == countryId).ToListAsync(cancellationToken: cancellationToken);
    }
}