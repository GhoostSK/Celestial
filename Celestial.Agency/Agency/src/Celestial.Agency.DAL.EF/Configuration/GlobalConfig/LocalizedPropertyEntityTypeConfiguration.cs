using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public class LocalizedPropertyEntityTypeConfiguration : IEntityTypeConfiguration<LocalizedProperty>
{
    public void Configure(EntityTypeBuilder<LocalizedProperty> builder)
    {
        builder.ToTable("LocalizedProperties");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LocalKeyGroup)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.LocalKey)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.LocalValue)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.Language)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");

        builder.Property(x => x.EntityId).IsRequired();

        builder.HasIndex(x => new { x.LocalKeyGroup, x.Language, x.LocalKey, x.EntityId })
            .HasDatabaseName("IX_LocalizedProperty_Search");
    }
}