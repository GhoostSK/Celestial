using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public sealed class LanguageEntityTypeConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable(nameof(Language));
        builder.Property(p => p.Name).HasMaxLength(10).IsRequired();
        builder.Property(p => p.LanguageCulture).HasMaxLength(20).IsRequired();
        builder.Property(p => p.Published).IsRequired();
        builder.Property(p => p.Rtl).IsRequired();
        builder.Property(p => p.DisplayOrder).IsRequired();
    }
}