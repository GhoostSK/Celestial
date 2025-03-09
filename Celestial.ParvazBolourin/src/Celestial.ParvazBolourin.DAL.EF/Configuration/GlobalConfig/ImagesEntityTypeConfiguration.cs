using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.GlobalConfig;
public sealed class ImagesEntityTypeConfiguration : IEntityTypeConfiguration<Images>
{
    public void Configure(EntityTypeBuilder<Images> builder)
    {
        builder.ToTable(nameof(Images));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.KeyGroup)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.EntityId)
            .IsRequired();

        builder.Property(p => p.PicturePath)
            .IsRequired()
            .HasMaxLength(500)
            .HasComment("Path to the stored image.");

        builder.Property(p => p.DisplayOrder)
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Order of the image for display purposes.");

        builder.HasIndex(p => new { p.KeyGroup, p.EntityId })
            .HasDatabaseName("IX_Images_KeyGroup_EntityId");

        builder.HasIndex(p => p.PicturePath)
            .HasDatabaseName("IX_Images_PicturePath");
    }
}
