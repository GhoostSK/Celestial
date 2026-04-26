using Celestial.Agency.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class ToursEntityTypeConfiguration : IEntityTypeConfiguration<Tours>
{
    public void Configure(EntityTypeBuilder<Tours> builder)
    {
        builder.ToTable("Tours");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TourCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.TourStatus)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.Countries);
        builder.Property(x => x.Cities);
        builder.Property(x => x.ForeignCurrency)
            .HasMaxLength(10);

        builder.Property(x => x.BasePrice).HasPrecision(18, 2);
        builder.Property(x => x.ForeignPrice).HasPrecision(18, 2);

        builder.Property(x => x.TourScope)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.TourType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.TourContinent)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
        builder
            .HasMany(x => x.NecessaryEquipment);

        builder
            .HasMany(x => x.UnNecessaryEquipment);

        builder.HasMany(x => x.NecessaryEquipment).WithMany();
        builder.HasMany(x => x.UnNecessaryEquipment).WithMany();
        builder.HasIndex(x => x.TourCode).IsUnique();
        builder.HasIndex(x => x.TourStatus);
        builder.HasIndex(x => x.TourScope);
    }
}