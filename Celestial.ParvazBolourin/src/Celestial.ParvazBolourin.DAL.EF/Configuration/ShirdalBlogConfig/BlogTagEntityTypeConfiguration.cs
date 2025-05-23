using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.ShirdalBlogConfig;

public sealed class BlogTagEntityTypeConfiguration : IEntityTypeConfiguration<BlogTags>
{
    public void Configure(EntityTypeBuilder<BlogTags> builder)
    {
        builder.ToTable("BlogTags");

        builder.Property(p => p.ViewCount)
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Number of Views for the blog Tag.");

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}