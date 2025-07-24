using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.Agency.Core.DAL.Model.ShirdalBlog;

public sealed class BlogCategory : DbBaseEntity<long>
{
    public bool IsDeleted { get; set; } = false;
}