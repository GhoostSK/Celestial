using Celestial.Agency.Core.DAL.Model.Global;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Comment;

public sealed class GetCommentsByEntityIdCommandReq : IRequest<GetCommentsByEntityCommandRes>
{
    public CommentKeyGroupType KeyGroup { get; set; }
    public long EntityId { get; set; }
}

public sealed class GetCommentsByEntityCommandRes : ServiceResContextBase
{
    public List<CommentItemDto> Items { get; set; } = new();
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