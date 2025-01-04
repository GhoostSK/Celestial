using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.HotelConfig
{
    public sealed class HotelPictureMappingEntityTypeConfiguration : IEntityTypeConfiguration<HotelPicture>
    {
        public void Configure(EntityTypeBuilder<HotelPicture> builder)
        {
            ArgumentNullException.ThrowIfNull(nameof(builder));

            builder.ToTable(nameof(HotelPicture));

            builder.Property(p => p.DisplayOrder)
                .IsRequired();

            builder.HasIndex(p => p.DisplayOrder);

            builder.Property(p => p.VirtualPath)
                .HasMaxLength(500) 
                .IsRequired();
        }
    }
}