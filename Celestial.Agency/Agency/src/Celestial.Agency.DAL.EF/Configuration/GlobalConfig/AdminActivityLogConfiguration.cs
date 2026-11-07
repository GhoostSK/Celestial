using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public class AdminActivityLogEntityTypeConfiguration : IEntityTypeConfiguration<AdminActivityLog>
{
    public void Configure(EntityTypeBuilder<AdminActivityLog> builder)
    {
        builder.ToTable(nameof(AdminActivityLog));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Action)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.EntityType)
            .HasMaxLength(100);

        builder.Property(x => x.Details)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.UserId);
    }
}