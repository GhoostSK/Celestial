using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class TourEquipmentsEntityTypeConfiguration : IEntityTypeConfiguration<TourEquipments>
{
    public void Configure(EntityTypeBuilder<TourEquipments> builder)
    {
        builder.ToTable("TourEquipments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Icon)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}