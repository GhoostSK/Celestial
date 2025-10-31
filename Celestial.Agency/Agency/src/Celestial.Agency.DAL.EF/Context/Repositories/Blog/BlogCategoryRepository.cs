using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Blog;

public sealed class BlogCategoryRepository(MainDbContext dbContext)
    : RepositoryBase<BlogCategory>(dbContext), IBlogCategoryService
{
    public async Task<List<BlogCategory>> GetBlogCategoriesList(int count, CancellationToken cancellationToken)
    {
        var query = TableNoTracking
            .IgnoreQueryFilters();

        var result = count > 0
            ? await query.Take(count).ToListAsync(cancellationToken)
            : await query.ToListAsync(cancellationToken);

        return result;
    }

    public async Task<List<BlogCategory>> GetBlogCategoriesByIdsAsync(List<long> ids,
        CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}