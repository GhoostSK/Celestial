using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Service.Command.Comment;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Comment;

public sealed class GetCommentsBySlugCommandHandler(
    ILocalizedPropertyService localizedPropertyService,
    ICommentService commentService
) : IRequestHandler<GetCommentsBySlugCommandReq, GetCommentsBySlugCommandRes>
{
    public async Task<GetCommentsBySlugCommandRes> Handle(GetCommentsBySlugCommandReq request,
        CancellationToken cancellationToken)
    {
        // 1) Resolve EntityId از روی Slug و LocalGroup
        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(
            p => p.LocalKey == "Slug"
                 && p.LocalValue == request.Slug
                 && p.LocalKeyGroup == request.LocalGroup,
            cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetCommentsBySlugCommandRes
            {
                Error = ValidationError.UnhandledValidationError("404", "Entity not found with slug")
            };

        var entityId = localizedProperty.EntityId;

        if (!Enum.TryParse<CommentKeyGroupType>(
                Enum.GetName(typeof(LocalKeyGroupType), request.LocalGroup)!,
                true,
                out var commentGroup))

            return new GetCommentsBySlugCommandRes
            {
                Error = ValidationError.UnhandledValidationError("400",
                    $"Unsupported mapping for LocalGroup '{request.LocalGroup}'"),
                LocalizedProperty = localizedProperty,
                EntityId = entityId
            };


        var comments = await commentService.GetCommentsByEntityAsync(commentGroup, entityId, cancellationToken);

        if (comments == null)
            return new GetCommentsBySlugCommandRes
            {
                Error = ValidationError.UnhandledValidationError("404", "Comment Not Found"),
                LocalizedProperty = localizedProperty,
                EntityId = entityId
            };

        var items = comments.Select(c => new CommentItemDto
        {
            Id = c.Id,
            CommentText = c.CommentText,
            UserId = c.UserId,
            IsConfirmed = c.IsConfirmed,
            ParentCommentId = c.ParentCommentId,
            RegisteredAt = c.RegisteredAt,
            Replies = c.Replies.Select(r => new CommentItemDto
            {
                Id = r.Id,
                CommentText = r.CommentText,
                UserId = r.UserId,
                IsConfirmed = r.IsConfirmed,
                ParentCommentId = r.ParentCommentId,
                RegisteredAt = r.RegisteredAt
            }).ToList()
        }).ToList();

        return new GetCommentsBySlugCommandRes
        {
            Items = items,
            LocalizedProperty = localizedProperty,
            EntityId = entityId
        };
    }
}