using Celestial.Agency.Core.DAL.Model.Faq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.FaqConfig;

public sealed class FaqEntityTypeConfiguration : IEntityTypeConfiguration<Faq>
{
    public void Configure(EntityTypeBuilder<Faq> builder)
    {
        builder.ToTable(nameof(Faq));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.Property(x => x.Published)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.DisplayOrder).HasDatabaseName("IX_Faq_DisplayOrder");
    }
}
