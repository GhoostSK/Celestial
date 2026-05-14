using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetCommentsBySlugReqDto
{
    public string Language { get; set; } = "fa";

    public required string Slug { get; init; }

    public required LocalKeyGroupType LocalGroup { get; set; }
}

public sealed class GetCommentsBySlugResDto : ServiceResContextBase
{
    public List<CommentItemDto> Items { get; init; } = new();
    public long? EntityId { get; init; }
}

public sealed class CommentItemDto
{
    public long Id { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
    public long? ParentCommentId { get; set; }
    public List<CommentItemDto> Replies { get; set; } = new();
}

public enum LocalKeyGroupType
{
    Blog,
    Hotel,
    Tour
}

public sealed class AddCommentReqDto
{
    public string Language { get; set; } = "fa";
    
    public LocalKeyGroupType KeyGroup { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string CommentText { get; set; } = string.Empty;
    public long? ParentCommentId { get; set; }
}

public sealed class AddCommentResDto : ServiceResContextBase
{
    public long Id { get; set; }
}