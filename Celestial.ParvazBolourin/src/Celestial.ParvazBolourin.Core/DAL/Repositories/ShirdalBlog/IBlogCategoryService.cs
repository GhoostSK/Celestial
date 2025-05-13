using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;

public interface IBlogCategoryService : IRepositoryEF<BlogCategory>, IScopeLifetime
{
    public Task<List<BlogCategory>> GetBlogCategoriesList(CancellationToken cancellationToken);
    Task<List<BlogCategory>> GetBlogCategoriesByIdsAsync(List<long> ids, CancellationToken cancellationToken);

}