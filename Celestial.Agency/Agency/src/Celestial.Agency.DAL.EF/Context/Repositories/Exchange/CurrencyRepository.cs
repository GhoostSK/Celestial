using Celestial.Agency.Core.DAL.Model.Exchange;
using Celestial.Agency.Core.DAL.Repositories.Exchange;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Exchange;

public sealed class CurrencyRepository(MainDbContext dbContext) : RepositoryBase<Currency>(dbContext), ICurrencyService
{
    public async Task<List<Currency>> GetCurrencyList(CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .ToListAsync(cancellationToken);
    }
}