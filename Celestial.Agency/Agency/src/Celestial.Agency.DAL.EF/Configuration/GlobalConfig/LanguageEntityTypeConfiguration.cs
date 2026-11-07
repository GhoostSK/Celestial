using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public class LanguageEntityTypeConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable(nameof(Language));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.LanguageCulture)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");

        builder.Property(x => x.Rtl)
            .HasDefaultValue(false);

        builder.Property(x => x.Published)
            .HasDefaultValue(true);

        builder.Property(x => x.DisplayOrder)
            .HasDefaultValue(0);

        builder.HasIndex(x => x.LanguageCulture)
            .IsUnique()
            .HasDatabaseName("IX_Language_Culture");
    }
}