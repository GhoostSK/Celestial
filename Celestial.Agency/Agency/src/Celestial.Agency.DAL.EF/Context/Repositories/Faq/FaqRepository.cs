using Celestial.Agency.Core.DAL.Repositories.Faq;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Faq;

public sealed class FaqRepository(MainDbContext dbContext)
    : RepositoryBase<Core.DAL.Model.Faq.Faq>(dbContext), IFaqService
{
    public async Task<List<Core.DAL.Model.Faq.Faq>> GetFaqList(bool onlyPublished,
        CancellationToken cancellationToken)
    {
        var query = TableNoTracking.Where(p => !p.IsDeleted);

        if (onlyPublished)
            query = query.Where(p => p.Published);

        return await query
            .OrderBy(p => p.DisplayOrder)
            .ThenBy(p => p.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Core.DAL.Model.Faq.Faq?> GetFaqById(long id, CancellationToken cancellationToken)
    {
        return await Table
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);
    }
}
