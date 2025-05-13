using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.ShirdalBlogConfig;

public sealed class BlogCategoryEntityTypeConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.ToTable(nameof(BlogCategory));

        builder
            .HasMany(p => p.Blogs)
            .WithMany(p => p.Categories)
            .UsingEntity(j => j.ToTable("Blog_BlogCategory"));
    }
}