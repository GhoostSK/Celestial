using Celestial.Agency.Core.DAL.Model.Exchange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.CurrencyConfig;

public class CurrencyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable(nameof(Currency));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.ExchangeRate)
            .HasPrecision(18, 4) // بهینه برای نرخ ارز
            .IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
    }
}