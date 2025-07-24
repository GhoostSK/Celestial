using Celestial.Agency.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class TourPrimaryTransportEntityTypeConfiguration : IEntityTypeConfiguration<TourPrimaryTransport>
{
    public void Configure(EntityTypeBuilder<TourPrimaryTransport> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable(nameof(TourPrimaryTransport));

        builder.Property(p => p.Date)
            .IsRequired();

        builder.Property(p => p.ReturnFlightCode)
            .IsRequired();

        builder.Property(p => p.LeadFlightCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ReturnStartTime)
            .IsRequired();

        builder.Property(p => p.ReturnEndTimeTime)
            .IsRequired();

        builder.Property(p => p.LeadStartTime)
            .IsRequired();

        builder.Property(p => p.LeadEndTime)
            .IsRequired();

        builder.HasIndex(p => p.LeadFlightCode);
    }
}