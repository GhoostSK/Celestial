using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour.TourEquipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.TourConfig.TourRequiredItemsConfig;

public class TourRequiredItemsConfig
{
    public class TourEquipmentEntityTypeConfiguration : IEntityTypeConfiguration<TourEquipments>
    {
        public void Configure(EntityTypeBuilder<TourEquipments> builder)
        {
            builder.ToTable(nameof(Tours));
            builder.Property(p => p.Icon).HasColumnType("NVARCHAR(MAX)").IsRequired();
        }
    }
}