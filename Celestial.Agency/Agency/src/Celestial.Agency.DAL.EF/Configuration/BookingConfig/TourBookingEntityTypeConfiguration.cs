using Celestial.Agency.Core.DAL.Model.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.BookingConfig;

public class TourBookingEntityTypeConfiguration : IEntityTypeConfiguration<TourBooking>
{
    public void Configure(EntityTypeBuilder<TourBooking> builder)
    {
        builder.ToTable(nameof(TourBooking));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookingCode)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("nvarchar(20)");

        builder.Property(x => x.TourId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasMaxLength(450)
            .HasColumnType("nvarchar(450)");

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("nvarchar(20)");

        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(x => x.NationalCode)
            .HasMaxLength(10)
            .HasColumnType("nvarchar(10)");

        builder.Property(x => x.Note)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.Currency)
            .HasMaxLength(10)
            .HasColumnType("nvarchar(10)");

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.BookingCode)
            .IsUnique()
            .HasDatabaseName("IX_TourBooking_BookingCode");

        builder.HasIndex(x => x.TourId).HasDatabaseName("IX_TourBooking_TourId");
    }
}
