using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;

public sealed class Blog : DbBaseEntity<long>
{
    public int Likes { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public List<long> CategoriesId { get; set; } = new();
    public List<BlogTagsMap> Tags { get; set; } = new();
}

public sealed class BlogTags : DbBaseEntity<long>
{
    public int ViewCount { get; set; } = 0;
    public bool IsDeleted { get; set; } = false;
    public List<BlogTagsMap> BlogTagsMaps { get; set; } = new();
}

public class BlogTagsMap : DbBaseEntity<long>
{
    public long BlogId { get; set; }
    public Blog Blog { get; set; }

    public long BlogTagId { get; set; }
    public BlogTags BlogTag { get; set; }
}
