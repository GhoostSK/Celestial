using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;

public interface IBlogService : IRepositoryEF<Blog>, IScopeLifetime
{
    public Task<long> AddBlog(Blog blog, CancellationToken cancellationToken);

    public Task<(List<Blog> Items, int TotalPages, int totalRecord)> GetBlogsList(List<long> categoryIds,
        List<long> tagsId, int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<Blog?> GetBlogById(long id, CancellationToken cancellationToken);

    public Task<List<Blog>> GetTopBlogsByLikes(int count, CancellationToken cancellationToken);
}