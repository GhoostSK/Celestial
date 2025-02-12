using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.TourConfig;

public class TourEntityTypeConfiguration : IEntityTypeConfiguration<Tours>
{
    public void Configure(EntityTypeBuilder<Tours> builder)
    {
        builder.ToTable(nameof(Tours));

        builder.Property(p => p.Countries)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(t => t.TourContinent)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Score)
            .IsRequired();

        builder.Property(p => p.HotelCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.ImportantTips)
            .IsRequired();

        builder.Property(p => p.PassengerCapacity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.TourStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.Hotels)
            .IsRequired();

        builder.Property(p => p.StartDate)
            .IsRequired();

        builder.Property(p => p.PicturePath)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(p => p.PrimaryTransport)
            .WithMany()
            .HasForeignKey(t => t.Id)
            .OnDelete(DeleteBehavior.Restrict);

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

        builder.Property(p => p.DayCount)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.RequiresVisa);
    }
}