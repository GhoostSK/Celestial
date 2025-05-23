using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.ShirdalBlog;

public sealed class BlogRepository(MainDbContext dbContext) : RepositoryBase<Blog>(dbContext), IBlogService
{
    public async Task<Blog?> GetBlogById(long id, CancellationToken cancellationToken)
    {
        return await Table
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false, cancellationToken);
    }
    public async Task<(List<Blog> Items, int TotalPages)> GetBlogsList(List<long> categoryIds,List<long> tagsId,int pageNumber,int pageSize,CancellationToken cancellationToken)
    {
        var query = TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => !p.IsDeleted);

        if (categoryIds.Any())
            query = query.Where(p => p.CategoriesId.Any(id => categoryIds.Contains(id)));

        if (tagsId.Any())
            query = query.Where(p => p.TagsId.Any(id => tagsId.Contains(id)));

        var totalItems = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalPages);
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