using Celestial.Agency.Core.DAL.Model.Geo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GeoConfig;

public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Shortname)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnType("varchar(3)");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(x => x.PhoneCode)
            .IsRequired();

        builder.HasIndex(x => x.Shortname).IsUnique();
    }
}