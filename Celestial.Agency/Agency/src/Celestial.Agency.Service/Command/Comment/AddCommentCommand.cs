using Celestial.Agency.Core.DAL.Model.Global;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Comment;

public sealed class AddCommentCommandReq : IRequest<AddCommentCommandRes>
{
    public CommentKeyGroupType KeyGroup { get; set; }
    public long EntityId { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public long? ParentCommentId { get; set; }
}

public sealed class AddCommentCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
}