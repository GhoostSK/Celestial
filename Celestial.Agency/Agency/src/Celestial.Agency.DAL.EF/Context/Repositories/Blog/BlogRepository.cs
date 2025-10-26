using Celestial.Agency.Core.DAL.Repositories.Blog;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Blog;

public sealed class BlogRepository(MainDbContext dbContext)
    : RepositoryBase<Core.DAL.Model.ShirdalBlog.Blog>(dbContext), IBlogService
{
    public async Task<Core.DAL.Model.ShirdalBlog.Blog?> GetBlogById(long id, CancellationToken cancellationToken)
    {
        return await Table
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<long> AddBlog(Core.DAL.Model.ShirdalBlog.Blog blog, CancellationToken cancellationToken)
    {
        await AddAsync(blog, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        return blog.Id;
    }

    public async Task<List<Core.DAL.Model.ShirdalBlog.Blog>> GetTopBlogsByLikes(int count,
        CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => p.IsDeleted == false)
            .OrderByDescending(p => p.Likes)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<(List<Core.DAL.Model.ShirdalBlog.Blog> Items, int TotalPages, int TotalRecord)> GetBlogsList(
        long categoryId,
        List<long> tagsId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = TableNoTracking
            .IgnoreQueryFilters();

        // فیلتر دسته
        if (categoryId > 0)
            query = query.Where(p => p.CategoryId == categoryId);

        // فیلتر تگ‌ها
        if (tagsId != null && tagsId.Any())
            query = query.Where(p => p.TagsId.Any(id => tagsId.Contains(id)));

        var totalItems = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var totalRecord = await Table
            .IgnoreQueryFilters()
            .CountAsync(cancellationToken);

        return (items, totalPages, totalRecord);
    }

    public async Task<List<Core.DAL.Model.ShirdalBlog.Blog>> GetLatestBlogs(int count,
        CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .OrderByDescending(p => p.RegistrationDate)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}