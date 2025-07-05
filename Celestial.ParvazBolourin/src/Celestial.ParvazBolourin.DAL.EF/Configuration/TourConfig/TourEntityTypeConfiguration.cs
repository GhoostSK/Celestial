using System.Text.Json;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.TourConfig;

public class TourEntityTypeConfiguration : IEntityTypeConfiguration<Tours>
{
    public void Configure(EntityTypeBuilder<Tours> builder)
    {
        builder.ToTable(nameof(Tours));

        builder.Property(p => p.CountriesId)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.CountriesId)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(t => t.TourContinent)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Score)
            .IsRequired();

        builder.Property(p => p.TourCode)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .HasIndex(p => p.TourCode)
            .IsUnique();

        builder.Property(p => p.PassengerCapacity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.TourStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.Hotels)
            .IsRequired();

        builder.Property(p => p.StartDate);

        builder.Property(p => p.RegistrationDeadline);

        builder.Property(p => p.PictureId);

        builder.Property(p => p.BasePrice)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.ForeignCurrency)
            .HasMaxLength(100);

        builder.Property(p => p.ForeignPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.TotalDay).IsRequired();


        builder.HasOne(p => p.PrimaryTransport)
            .WithMany()
            .HasForeignKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.NecessaryEquipment).IsRequired(false).HasMaxLength(200);
        builder.Property(p => p.UnNecessaryEquipment).IsRequired(false).HasMaxLength(200);

        builder.HasMany(p => p.TransPorts)
            .WithOne()
            .HasForeignKey(t => t.TourId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.TourPlans)
            .WithOne()
            .HasForeignKey(t => t.TourId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.TourType)
            .HasConversion<string>();

        builder.Property(t => t.TourContinent)
            .HasConversion<string>();

        builder.Property(t => t.TourScope)
            .HasConversion<string>();

        builder.Property(t => t.ExecDate)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                v => JsonSerializer.Deserialize<List<DateOnly>>(v, (JsonSerializerOptions)null!) ?? new List<DateOnly>()
            ).IsRequired().HasColumnType("NVARCHAR(MAX)");

        builder.Property(p => p.RequiresVisa);
        builder.Property(p => p.Views).HasColumnType("bigint");
    }
}