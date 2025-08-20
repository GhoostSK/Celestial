using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public sealed class CommentsEntityTypeConfiguration : IEntityTypeConfiguration<Comments>
{
    public void Configure(EntityTypeBuilder<Comments> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable(nameof(Comments));
        builder.Property(p => p.CommentText).HasMaxLength(int.MaxValue).IsRequired();
        builder.Property(p => p.IsConfirmed).IsRequired();
        builder.Property(p => p.UserId).HasMaxLength(128).IsRequired();
    }
}