using Celestial.Agency.Core.DAL.Model.ShirdalBlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.ShirdalBlogConfig;

public class BlogCategoryEntityTypeConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.ToTable("BlogCategories");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}