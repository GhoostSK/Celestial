using Celestial.Agency.Core.DAL.Model.Newsletter;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Newsletter;

public interface INewsletterSubscriptionService : IRepositoryEF<NewsletterSubscription>, IScopeLifetime
{
    public Task<NewsletterSubscription?> GetByEmail(string email, CancellationToken cancellationToken);

    public Task<(List<NewsletterSubscription> Items, int TotalPages, int TotalRecord)> GetSubscriptionList(
        bool onlyActive, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
