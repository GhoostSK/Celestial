using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.ShirdalBlogConfig;

public class BlogTagsEntityTypeConfiguration : IEntityTypeConfiguration<BlogTags>
{
    public void Configure(EntityTypeBuilder<BlogTags> builder)
    {
        builder.ToTable("BlogTags");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ViewCount)
            .HasDefaultValue(0);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasIndex(x => x.ViewCount);
    }
}