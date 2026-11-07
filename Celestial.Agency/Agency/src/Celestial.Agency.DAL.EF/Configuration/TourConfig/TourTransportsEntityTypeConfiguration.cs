using Celestial.Agency.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class TourTransPortsEntityTypeConfiguration : IEntityTypeConfiguration<TourTransPort>
{
    public void Configure(EntityTypeBuilder<TourTransPort> builder)
    {
        builder.ToTable(nameof(TourTransPort));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TourId).IsRequired();

        builder.Property(x => x.TransPortType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(x => x.TourId);
    }
}