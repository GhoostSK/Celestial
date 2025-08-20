using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public sealed class LocalizedPropertyEntityTypeConfiguration : IEntityTypeConfiguration<LocalizedProperty>
{
    public void Configure(EntityTypeBuilder<LocalizedProperty> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable(nameof(LocalizedProperty));
        builder.Property(p => p.EntityId).IsRequired();
        builder.Property(p => p.LocalKey).HasMaxLength(40).IsRequired();
        builder.Property(p => p.Language).IsRequired();
        builder.Property(p => p.LocalKeyGroup).HasConversion(new EnumToStringConverter<LocalKeyGroupType>());
        ;
        builder.Property(p => p.LocalValue).HasMaxLength(int.MaxValue).IsRequired();
    }
}