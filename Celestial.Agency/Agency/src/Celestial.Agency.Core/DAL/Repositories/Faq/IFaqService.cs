using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Faq;

public interface IFaqService : IRepositoryEF<Model.Faq.Faq>, IScopeLifetime
{
    public Task<List<Model.Faq.Faq>> GetFaqList(bool onlyPublished, CancellationToken cancellationToken);

    public Task<Model.Faq.Faq?> GetFaqById(long id, CancellationToken cancellationToken);
}
