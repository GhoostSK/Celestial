using Celestial.Agency.Core.DAL.Model.Global;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Comment;

public interface ICommentService : IRepositoryEF<Comments>, IScopeLifetime
{
    Task<List<Comments>> GetCommentsByEntityAsync(CommentKeyGroupType keyGroup, long entityId,
        CancellationToken cancellationToken);

    Task<Comments?> GetCommentByIdAsync(long id, CancellationToken cancellationToken);
}