using Celestial.Agency.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.TourConfig;

public class TourPlansEntityTypeConfiguration : IEntityTypeConfiguration<TourPlans>
{
    public void Configure(EntityTypeBuilder<TourPlans> builder)
    {
        builder.ToTable("TourPlans");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TourId).IsRequired();

        builder.Property(x => x.PlanPictureId)
            .HasConversion(
                v => string.Join(",", v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList())
            .HasColumnType("varchar(max)");
    }
}