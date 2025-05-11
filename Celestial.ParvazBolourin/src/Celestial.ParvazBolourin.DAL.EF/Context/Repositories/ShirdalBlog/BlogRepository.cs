using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.ShirdalBlog;

public sealed class BlogRepository(MainDbContext dbContext) : RepositoryBase<Blog>(dbContext), IBlogService
{
    public async Task<Blog?> GetBlogById(int id, CancellationToken cancellationToken)
    {
        return await Table
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false, cancellationToken);
    }

    public async Task<List<Blog>> GetBlogsList(CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => p.IsDeleted == false)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> AddBlog(Blog blog, CancellationToken cancellationToken)
    {
        await AddAsync(blog, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        return blog.Id;
    }

    public async Task<List<Blog>> GetTopBlogsByLikes(int count, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => p.IsDeleted == false)
            .OrderByDescending(p => p.Likes)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}