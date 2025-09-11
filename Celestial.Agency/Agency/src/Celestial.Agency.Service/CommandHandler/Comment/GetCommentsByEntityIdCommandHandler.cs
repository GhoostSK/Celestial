using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.Service.Command.Comment;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Comment;

public sealed class GetCommentsByEntityIdCommandHandler(ICommentService commentService)
    : IRequestHandler<GetCommentsByEntityIdCommandReq, GetCommentsByEntityCommandRes>
{
    public async Task<GetCommentsByEntityCommandRes> Handle(GetCommentsByEntityIdCommandReq request,
        CancellationToken cancellationToken)
    {
        var comments =
            await commentService.GetCommentsByEntityAsync(request.KeyGroup, request.EntityId, cancellationToken);

        if (comments == null)
            return new GetCommentsByEntityCommandRes
            {
                Error = ValidationError.UnhandledValidationError("404", "Comment Not Found")
            };
        var items = comments.Select(c => new CommentItemDto
        {
            Id = c.Id,
            CommentText = c.CommentText,
            UserId = c.UserId,
            IsConfirmed = c.IsConfirmed,
            ParentCommentId = c.ParentCommentId,
            Replies = c.Replies.Select(r => new CommentItemDto
            {
                Id = r.Id,
                CommentText = r.CommentText,
                UserId = r.UserId,
                IsConfirmed = r.IsConfirmed,
                ParentCommentId = r.ParentCommentId
            }).ToList()
        }).ToList();

        return new GetCommentsByEntityCommandRes { Items = items };
    }
}