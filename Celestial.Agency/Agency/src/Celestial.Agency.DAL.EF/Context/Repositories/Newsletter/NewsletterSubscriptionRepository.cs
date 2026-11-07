using Celestial.Agency.Core.DAL.Model.Newsletter;
using Celestial.Agency.Core.DAL.Repositories.Newsletter;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Newsletter;

public sealed class NewsletterSubscriptionRepository(MainDbContext dbContext)
    : RepositoryBase<NewsletterSubscription>(dbContext), INewsletterSubscriptionService
{
    public async Task<NewsletterSubscription?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await Table
            .FirstOrDefaultAsync(p => p.Email == email && !p.IsDeleted, cancellationToken);
    }

    public async Task<(List<NewsletterSubscription> Items, int TotalPages, int TotalRecord)> GetSubscriptionList(
        bool onlyActive, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = TableNoTracking.Where(p => !p.IsDeleted);

        if (onlyActive)
            query = query.Where(p => p.IsActive);

        var totalRecord = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecord / (double)pageSize);

        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalPages, totalRecord);
    }
}
