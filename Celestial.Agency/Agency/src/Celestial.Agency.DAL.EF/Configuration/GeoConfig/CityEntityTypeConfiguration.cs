using Celestial.Agency.Core.DAL.Model.Geo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GeoConfig;

public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(x => x.State_id)
            .IsRequired();

        builder.Property(x => x.country_id)
            .IsRequired();

        builder.HasIndex(x => new { x.Name, x.country_id })
            .HasDatabaseName("IX_City_Name_Country");
    }
}