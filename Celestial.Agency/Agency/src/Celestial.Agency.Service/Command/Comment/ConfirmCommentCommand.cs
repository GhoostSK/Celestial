using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Comment;

public sealed class ConfirmCommentCommandReq : IRequest<ConfirmCommentCommandRes>
{
    public long EntityId { get; set; }
}

public sealed class ConfirmCommentCommandRes : ServiceResContextBase
{
}