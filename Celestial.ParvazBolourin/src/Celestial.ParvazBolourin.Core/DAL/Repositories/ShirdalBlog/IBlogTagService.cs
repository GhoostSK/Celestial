using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;

public interface IBlogTagService : IRepositoryEF<BlogTags>, IScopeLifetime
{
}