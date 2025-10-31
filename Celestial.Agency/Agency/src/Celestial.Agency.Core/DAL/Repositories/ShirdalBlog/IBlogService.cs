using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Blog;

public interface IBlogService : IRepositoryEF<Model.ShirdalBlog.Blog>, IScopeLifetime
{
    public Task<long> AddBlog(Model.ShirdalBlog.Blog blog, CancellationToken cancellationToken);

    public Task<(List<Model.ShirdalBlog.Blog> Items, int TotalPages, int TotalRecord)> GetBlogsList(long categoryId,
        List<long> tagsId,
        int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<Model.ShirdalBlog.Blog?> GetBlogById(long id, CancellationToken cancellationToken);

    public Task<List<Model.ShirdalBlog.Blog>> GetTopBlogsByLikes(int count, CancellationToken cancellationToken);

    public Task<List<Model.ShirdalBlog.Blog>> GetLatestBlogs(int count,
        CancellationToken cancellationToken);
}