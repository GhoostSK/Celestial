using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;

public sealed class BlogTags : DbBaseEntity<long>
{
    public int ViewCount { get; set; } = 0;
    public bool IsDeleted { get; set; } = false;
}
