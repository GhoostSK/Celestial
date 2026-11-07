using Celestial.Agency.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class TourPrimaryTransportEntityTypeConfiguration : IEntityTypeConfiguration<TourPrimaryTransport>
{
    public void Configure(EntityTypeBuilder<TourPrimaryTransport> builder)
    {
        builder.ToTable(nameof(TourPrimaryTransport));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LeadFlightCode)
            .HasMaxLength(50);

        builder.Property(x => x.ReturnFlightCode)
            .HasMaxLength(50);

        builder.Property(x => x.AirLine)
            .HasMaxLength(100);

        builder.Property(x => x.AirLineImagePath)
            .HasMaxLength(200);

        builder.HasOne(x => x.Tour)
            .WithOne(t => t.PrimaryTransport)
            .HasForeignKey<TourPrimaryTransport>(x => x.TourId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}