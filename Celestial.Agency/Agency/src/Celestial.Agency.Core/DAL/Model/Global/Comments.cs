using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Global;

public sealed class Comments : DbBaseEntity<long>
{
    public CommentKeyGroupType KeyGroup { get; set; }
    public long EntityId { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
    public long? ParentCommentId { get; set; }
    public Comments? ParentComment { get; set; }
    public List<Comments> Replies { get; set; } = new();
    public DateTimeOffset RegisteredAt { get; set; } = DateTimeOffset.Now;
    public bool IsDeleted { get; set; }
}

public enum CommentKeyGroupType
{
    Blog,
    Hotel,
    Tour
}