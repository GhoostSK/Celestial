using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public sealed class CommentsRepository(MainDbContext dbContext) : RepositoryBase<Comments>(dbContext), ICommentsService
{
    public async Task AddComment(Comments model, CancellationToken cancellationToken)
    {
        await AddAsync(model, cancellationToken);
    }

    public async Task<List<Comments>> GetComments(long entityId, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .Where(p => p.EntityId == entityId && p.IsConfirmed == true)
            .Select(p => new Comments
            {
                CommentText = p.CommentText,
                EntityId = p.EntityId,
                UserId = p.UserId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task ConfirmComment(int commentId, CancellationToken cancellationToken)
    {
        var comment = await TableNoTracking
            .Where(p => p.Id == commentId)
            .FirstOrDefaultAsync(cancellationToken);
        comment!.IsConfirmed = true;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Comments>> GetCommentsToAdmin(int entityId, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .Where(p => p.EntityId == entityId)
            .ToListAsync(cancellationToken);
    }
}