using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public sealed class CommentsRepository(MainDbContext dbContext) : RepositoryBase<Core.DAL.Model.Global.Comment>(dbContext), ICommentsService
{
    public async Task AddComment(Core.DAL.Model.Global.Comment model, CancellationToken cancellationToken)
    {
        await AddAsync(model, cancellationToken);
    }

    public async Task<List<Core.DAL.Model.Global.Comment>> GetComments(long entityId, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => p.EntityId == entityId && p.IsConfirmed == true)
            .Select(p => new Core.DAL.Model.Global.Comment
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
            .IgnoreQueryFilters()
            .Where(p => p.Id == commentId)
            .FirstOrDefaultAsync(cancellationToken);
        comment!.IsConfirmed = true;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Core.DAL.Model.Global.Comment>> GetCommentsToAdmin(int entityId, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => p.EntityId == entityId)
            .ToListAsync(cancellationToken);
    }
}