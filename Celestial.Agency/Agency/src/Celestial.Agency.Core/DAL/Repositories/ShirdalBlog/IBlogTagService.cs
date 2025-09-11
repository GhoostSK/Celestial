using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.ShirdalBlog;

public interface IBlogTagService : IRepositoryEF<BlogTags>, IScopeLifetime
{
    Task<List<BlogTags>> GetTopTagsByViewCountAsync(int count, CancellationToken cancellationToken);

    public Task<List<BlogTags>> GetTagsList(int count, CancellationToken cancellationToken);
}