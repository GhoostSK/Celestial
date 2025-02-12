using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.ParvazBolourin.DAL.EF.Configuration.VisaConfig;

public sealed class VisaEntityTypeConfiguration : IEntityTypeConfiguration<Visas>
{
    public void Configure(EntityTypeBuilder<Visas> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable(nameof(Visas));

        builder.Property(p => p.PicturePath)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.VisaIssuer)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.IssueDate)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.HasIndex(p => p.Status);

        builder.HasIndex(p => p.Country);
    }
}