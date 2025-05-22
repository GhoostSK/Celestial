using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.ShirdalBlogConfig;

public class BlogTagsMapConfiguration : IEntityTypeConfiguration<BlogTagsMap>
{
    public void Configure(EntityTypeBuilder<BlogTagsMap> builder)
    {
        builder.HasKey(x => new { x.BlogId, x.BlogTagId });

        builder
            .HasOne(bt => bt.Blog)
            .WithMany(b => b.Tags)
            .HasForeignKey(bt => bt.BlogId);

        builder
            .HasOne(bt => bt.BlogTag)
            .WithMany(t => t.BlogTagsMaps)
            .HasForeignKey(bt => bt.BlogTagId);

        builder.ToTable("BlogTagsMap");
    }
}
