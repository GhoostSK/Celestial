using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.ShirdalBlog;

public sealed class BlogTagsRepository(MainDbContext dbContext) : RepositoryBase<BlogTags>(dbContext), IBlogTagService
{
    public async Task<List<BlogTags>> GetTopTagsByViewCountAsync(int count, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .Where(t => !t.IsDeleted)
            .OrderByDescending(t => t.ViewCount)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<BlogTags>> GetTagsList(CancellationToken cancellationToken)
    {
        var query = TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => !p.IsDeleted);

        return await query
            .ToListAsync(cancellationToken);
    }
}