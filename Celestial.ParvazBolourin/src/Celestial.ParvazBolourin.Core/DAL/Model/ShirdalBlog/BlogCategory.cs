using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;

public sealed class BlogCategory : DbBaseEntity<long>
{
    public List<Blog> Blogs { get; set; } = new();
    public bool IsDeleted { get; set; } = false;
}