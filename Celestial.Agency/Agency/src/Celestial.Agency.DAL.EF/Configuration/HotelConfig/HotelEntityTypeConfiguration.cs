using Celestial.Agency.Core.DAL.Model.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.HotelConfig;

public class HotelsEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable(nameof(Hotel));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(x => x.HotelLocation)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.HotelContinent)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.BookingUrl)
            .HasMaxLength(500)
            .HasColumnType("nvarchar(500)");

        builder.Property(x => x.HotelCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.HotelCoordinate)
            .HasMaxLength(150);

        builder.Property(x => x.HotelReception)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.HotelCode).IsUnique();
        builder.HasIndex(x => new { x.CountryId, x.CityId, x.StateId });

        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}