using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.ShirdalBlog;

public sealed class BlogTagsRepository(MainDbContext dbContext) : RepositoryBase<BlogTags>(dbContext), IBlogTagService
{

}