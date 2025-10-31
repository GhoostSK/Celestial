using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Comment;

public interface ICommentService : IRepositoryEF<Comments>, IScopeLifetime
{
    Task<List<Comments>> GetCommentsByEntityAsync(CommentKeyGroupType keyGroup, long entityId,
        CancellationToken cancellationToken);

    Task<Comments?> GetCommentByIdAsync(long id, CancellationToken cancellationToken);
}