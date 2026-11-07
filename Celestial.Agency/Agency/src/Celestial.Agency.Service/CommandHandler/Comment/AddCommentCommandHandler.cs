using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Service.Command.Comment;
using Celestial.SharedKernel.Common;
using Mapster;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Comment;

public sealed class AddCommentCommandHandler(
    ICommentService commentService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<AddCommentCommandReq, AddCommentCommandRes>
{
    public async Task<AddCommentCommandRes> Handle(AddCommentCommandReq request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.CommentText))
            return new AddCommentCommandRes
            {
                Error = ValidationError.UnhandledValidationError("400", "Comment text is required")
            };

        var entityId = request.EntityId;

        if (entityId == 0)
        {
            if (string.IsNullOrWhiteSpace(request.Slug))
                return new AddCommentCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("400", "Either EntityId or Slug is required")
                };

            if (!Enum.TryParse<LocalKeyGroupType>(
                    Enum.GetName(typeof(CommentKeyGroupType), request.KeyGroup)!, true, out var localGroup))
                return new AddCommentCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("400",
                        $"Unsupported mapping for KeyGroup '{request.KeyGroup}'")
                };

            var slugProperty = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.LocalKey == "Slug"
                     && p.LocalKeyGroup == localGroup
                     && p.LocalValue.ToLower() == request.Slug.ToLower(),
                cancellationToken: cancellationToken);

            if (slugProperty == null)
                return new AddCommentCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("404", "Entity not found with slug")
                };

            entityId = slugProperty.EntityId;
        }

        var entity = request.Adapt<Core.DAL.Model.Global.Comment>();
        entity.EntityId = entityId;

        await commentService.AddAsync(entity, cancellationToken);
        await commentService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddCommentCommandRes { Id = entity.Id };
    }
}
