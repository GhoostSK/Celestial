using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.Service.Command.Comment;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Comment;

public sealed class ConfirmCommentCommandHandler(ICommentService commentService)
    : IRequestHandler<ConfirmCommentCommandReq, ConfirmCommentCommandRes>
{
    public async Task<ConfirmCommentCommandRes> Handle(ConfirmCommentCommandReq request,
        CancellationToken cancellationToken)
    {
        var data = await commentService.FindByIdAsync(cancellationToken, request.EntityId);
        if (data == null)
            return new ConfirmCommentCommandRes
            {
                Error = ValidationError.UnhandledValidationError("403", "Comment Not Found")
            };
        data.IsConfirmed = true;
        await commentService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new ConfirmCommentCommandRes();
    }
}