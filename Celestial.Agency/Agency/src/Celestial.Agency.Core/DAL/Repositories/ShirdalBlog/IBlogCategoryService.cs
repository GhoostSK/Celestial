using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;

public interface IBlogCategoryService : IRepositoryEF<BlogCategory>, IScopeLifetime
{
    public Task<List<BlogCategory>> GetBlogCategoriesList(int count, CancellationToken cancellationToken);

    Task<List<BlogCategory>> GetBlogCategoriesByIdsAsync(List<long> ids, CancellationToken cancellationToken);
}