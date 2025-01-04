using Celestial.ParvazBolourin.Core.DAL.Model.Contact;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.Contact;

public class ContactUsEntityTypeConfiguration : IEntityTypeConfiguration<ContactUs>
{
    public void Configure(EntityTypeBuilder<ContactUs> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);
    }
}