using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Comment;

public sealed class CommentRepository(MainDbContext dbContext)
    : RepositoryBase<Core.DAL.Model.Global.Comment>(dbContext), ICommentService
{
    public async Task<List<Core.DAL.Model.Global.Comment>> GetCommentsByEntityAsync(CommentKeyGroupType keyGroup, long entityId,
        CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Include(x => x.Replies)
            .Where(x => x.KeyGroup == keyGroup && x.EntityId == entityId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Core.DAL.Model.Global.Comment?> GetCommentByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Include(x => x.Replies)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }
}