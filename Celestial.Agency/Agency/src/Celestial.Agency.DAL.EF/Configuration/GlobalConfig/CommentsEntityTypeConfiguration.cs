using Celestial.Agency.Core.DAL.Model.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celestial.Agency.DAL.EF.Configuration.GlobalConfig;

public class CommentsEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(Comment));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.KeyGroup)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.EntityId).IsRequired();

        builder.Property(x => x.CommentText)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.RegisteredAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(x => x.ParentComment)
            .WithMany(p => p.Replies)
            .HasForeignKey(x => x.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.KeyGroup, x.EntityId });
    }
}