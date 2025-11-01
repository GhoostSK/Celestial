using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Comment;

public sealed class GetCommentsBySlugCommandReq : IRequest<GetCommentsBySlugCommandRes>
{
    public required string Slug { get; init; }
    public required LocalKeyGroupType LocalGroup { get; set; }
}

public sealed class GetCommentsBySlugCommandRes : ServiceResContextBase
{
    public List<CommentItemDto> Items { get; init; } = new();
    public Core.DAL.Model.Global.LocalizedProperty? LocalizedProperty { get; set; }
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