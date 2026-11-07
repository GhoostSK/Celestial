using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class TourEquipmentsEntityTypeConfiguration : IEntityTypeConfiguration<TourEquipment>
{
    public void Configure(EntityTypeBuilder<TourEquipment> builder)
    {
        builder.ToTable(nameof(TourEquipment));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Icon)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}