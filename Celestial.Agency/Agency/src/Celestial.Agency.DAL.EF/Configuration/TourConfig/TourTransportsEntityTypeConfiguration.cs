using Celestial.Agency.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class TourTransportsEntityTypeConfiguration : IEntityTypeConfiguration<TourTransPorts>
{
    public void Configure(EntityTypeBuilder<TourTransPorts> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable(nameof(TourTransPorts));

        builder.Property(p => p.StartTime).IsRequired();
        builder.Property(p => p.EndTime).IsRequired();

        builder.Property(p => p.TransPortType)
            .IsRequired()
            .HasConversion<string>();
    }
}