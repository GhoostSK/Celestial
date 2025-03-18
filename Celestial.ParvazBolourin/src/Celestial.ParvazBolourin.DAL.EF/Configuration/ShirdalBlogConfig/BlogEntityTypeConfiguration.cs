using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.ShirdalBlogConfig;

public sealed class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
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
        //seo
        builder.Property(s => s.MetaTitle)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty)
            .IsRequired(false);

        builder.Property(s => s.MetaDescription)
            .HasMaxLength(500)
            .HasDefaultValue(string.Empty)
            .IsRequired(false);

        builder.Property(s => s.MetaKeywords)
            .HasMaxLength(500)
            .HasDefaultValue(string.Empty)
            .IsRequired(false);

        builder.Property(s => s.Slug)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}