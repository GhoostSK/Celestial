using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.ShirdalBlogConfig;

public sealed class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable(nameof(Blog));

        builder.Property(p => p.PicturePath)
            .IsRequired()
            .HasComment("Path to the picture associated with the blog.");

        builder.Property(p => p.Likes)
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Number of likes for the blog post.");

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Many-to-Many relation
        builder
            .HasMany(p => p.Categories)
            .WithMany(p => p.Blogs)
            .UsingEntity(j => j.ToTable("Blog_BlogCategory"));
    }
}