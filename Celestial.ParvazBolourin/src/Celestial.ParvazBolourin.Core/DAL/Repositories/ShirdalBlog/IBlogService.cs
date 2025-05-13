using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;

public interface IBlogService : IRepositoryEF<Blog>, IScopeLifetime
{
    public Task<long> AddBlog(Blog blog, CancellationToken cancellationToken);
    public Task<List<Blog>> GetBlogsList(List<long> categoryIds,CancellationToken cancellationToken);
    public Task<Blog?> GetBlogById(int id, CancellationToken cancellationToken);
    public Task<List<Blog>> GetTopBlogsByLikes(int count, CancellationToken cancellationToken);


}