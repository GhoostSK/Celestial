using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.Service.Command.Comment;
using Mapster;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Comment;

public sealed class AddCommentCommandHandler(ICommentService commentService)
    : IRequestHandler<AddCommentCommandReq, AddCommentCommandRes>
{
    public async Task<AddCommentCommandRes> Handle(AddCommentCommandReq request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Comments>();
        await commentService.AddAsync(entity, cancellationToken);
        await commentService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddCommentCommandRes { Id = entity.Id };
    }
}