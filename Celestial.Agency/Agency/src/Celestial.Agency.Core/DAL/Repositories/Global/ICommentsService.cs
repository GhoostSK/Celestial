using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Global;

public interface ICommentsService : IRepositoryEF<Comments>, IScopeLifetime
{
    public Task AddComment(Comments model, CancellationToken cancellationToken);

    public Task<List<Comments>> GetComments(long entityId, CancellationToken cancellationToken);

    public Task ConfirmComment(int commentId, CancellationToken cancellationToken);

    public Task<List<Comments>> GetCommentsToAdmin(int entityId, CancellationToken cancellationToken);
}