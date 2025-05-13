using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;

public sealed class Blog : DbBaseEntity<long>
{
    public int Likes { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public List<BlogCategory> Categories { get; set; } = new();
}

