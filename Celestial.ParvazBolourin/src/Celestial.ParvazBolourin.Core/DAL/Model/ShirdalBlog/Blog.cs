using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;

public sealed class Blog : DbBaseEntity<long>
{
    public int Likes { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

    //seo
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}