using Celestial.Agency.Core.DAL.Model.Contact;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.ContactUsConfig;

public class ContactUsEntityTypeConfiguration : IEntityTypeConfiguration<ContactUs>
{
    public void Configure(EntityTypeBuilder<ContactUs> builder)
    {
        builder.ToTable(nameof(ContactUs));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(x => x.Message)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.UserId)
            .HasColumnType("uniqueidentifier");

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.CreatedAt).HasDatabaseName("IX_ContactUs_CreatedAt");
    }
}