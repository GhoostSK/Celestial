using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public class ImagesEntityTypeConfiguration : IEntityTypeConfiguration<Images>
{
    public void Configure(EntityTypeBuilder<Images> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.KeyGroup)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.EntityId).IsRequired();

        builder.Property(x => x.PicturePath)
            .IsRequired()
            .HasMaxLength(300)
            .HasColumnType("nvarchar(300)");

        builder.Property(x => x.DisplayOrder).HasDefaultValue(0);

        builder.HasIndex(x => new { x.KeyGroup, x.EntityId });
    }
}