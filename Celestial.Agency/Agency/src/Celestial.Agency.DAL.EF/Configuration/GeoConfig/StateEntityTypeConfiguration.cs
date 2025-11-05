using Celestial.Agency.Core.DAL.Model.Geo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GeoConfig;

public class StateEntityTypeConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("States");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(x => x.Country_id)
            .IsRequired();

        builder.HasIndex(x => new { x.Name, x.Country_id })
            .HasDatabaseName("IX_State_Name_Country");
    }
}