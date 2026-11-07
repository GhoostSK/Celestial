using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Global;

public interface ICommentsService : IRepositoryEF<DAL.Model.Global.Comment>, IScopeLifetime
{
    public Task AddComment(DAL.Model.Global.Comment model, CancellationToken cancellationToken);

    public Task<List<DAL.Model.Global.Comment>> GetComments(long entityId, CancellationToken cancellationToken);

    public Task ConfirmComment(int commentId, CancellationToken cancellationToken);

    public Task<List<DAL.Model.Global.Comment>> GetCommentsToAdmin(int entityId, CancellationToken cancellationToken);
}