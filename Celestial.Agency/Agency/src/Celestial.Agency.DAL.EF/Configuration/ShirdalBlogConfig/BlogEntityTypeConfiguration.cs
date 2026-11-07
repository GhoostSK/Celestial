using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.ShirdalBlogConfig;

public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable(nameof(Blog));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Likes)
            .HasDefaultValue(0);

        builder.Property(x => x.PicturePath)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(x => x.CategoryId)
            .IsRequired();

        builder.Property(x => x.TagsId)
            .HasColumnType("varchar(max)");

        builder.Property(x => x.RegistrationDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.CategoryId);
    }
}