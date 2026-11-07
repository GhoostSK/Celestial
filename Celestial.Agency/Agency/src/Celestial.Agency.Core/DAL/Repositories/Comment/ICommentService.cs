using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Comment;

public interface ICommentService : IRepositoryEF<Model.Global.Comment>, IScopeLifetime
{
    Task<List<Model.Global.Comment>> GetCommentsByEntityAsync(CommentKeyGroupType keyGroup, long entityId,
        CancellationToken cancellationToken);

    Task<Model.Global.Comment?> GetCommentByIdAsync(long id, CancellationToken cancellationToken);
}