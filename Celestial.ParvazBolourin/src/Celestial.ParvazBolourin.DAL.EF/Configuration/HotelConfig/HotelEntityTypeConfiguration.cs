using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.HotelConfig;

public sealed class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotels>
{
    public void Configure(EntityTypeBuilder<Hotels> builder)
    {
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ToTable(nameof(Hotels));

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.HotelLocation)
                .HasMaxLength(100)
                .HasConversion<string>();

            builder.Property(p => p.HotelContinent)
                .HasMaxLength(100)
                .HasConversion<string>();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.HotelCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.CountryId)
                .IsRequired();

            builder.Property(p => p.CityId)
                .IsRequired();

            builder.Property(p => p.StateId)
                .IsRequired();

            builder.Property(p => p.BookingUrl)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(p => p.HotelScore)
                .IsRequired();

            builder.Property(p => p.HotelCoordinate)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(p => p.HotelReception)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .IsRequired();

            //seo
            builder.Property(s => s.MetaTitle)
                .HasMaxLength(200)
                .HasDefaultValue(string.Empty)
                .IsRequired(false);

            builder.Property(s => s.MetaDescription)
                .HasMaxLength(500)
                .HasDefaultValue(string.Empty)
                .IsRequired(false);

            builder.Property(s => s.MetaKeywords)
                .HasMaxLength(500)
                .HasDefaultValue(string.Empty)
                .IsRequired(false);

            builder.Property(s => s.Slug)
                .HasMaxLength(100)
                .IsRequired(false);

        }
    }
}