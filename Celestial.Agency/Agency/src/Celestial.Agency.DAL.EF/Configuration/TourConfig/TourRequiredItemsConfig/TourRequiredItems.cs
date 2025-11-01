using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig.TourRequiredItemsConfig;

public class TourRequiredItemsConfig
{
    public class TourEquipmentEntityTypeConfiguration : IEntityTypeConfiguration<TourEquipments>
    {
        public void Configure(EntityTypeBuilder<TourEquipments> builder)
        {
            builder.ToTable(nameof(TourEquipments));
            builder.Property(p => p.Icon).HasColumnType("NVARCHAR(MAX)").IsRequired();
        }
    }
}