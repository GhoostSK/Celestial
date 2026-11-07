using Celestial.Agency.Core.DAL.Model.Newsletter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.NewsletterConfig;

public sealed class NewsletterSubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<NewsletterSubscription>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscription> builder)
    {
        builder.ToTable(nameof(NewsletterSubscription));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(x => x.Language)
            .HasMaxLength(10)
            .HasColumnType("nvarchar(10)");

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasDatabaseName("IX_NewsletterSubscription_Email");
    }
}
