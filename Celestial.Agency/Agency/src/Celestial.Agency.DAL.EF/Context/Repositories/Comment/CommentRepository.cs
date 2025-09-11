using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Comment;

public sealed class CommentRepository(MainDbContext dbContext)
    : RepositoryBase<Comments>(dbContext), ICommentService
{
    public async Task<List<Comments>> GetCommentsByEntityAsync(CommentKeyGroupType keyGroup, long entityId,
        CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .Include(x => x.Replies)
            .Where(x => x.KeyGroup == keyGroup && x.EntityId == entityId && !x.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<Comments?> GetCommentByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .Include(x => x.Replies)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }
}