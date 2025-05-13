using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Global;

public interface ICommentsService : IRepositoryEF<Comments>, IScopeLifetime
{
    public Task AddComment(Comments model, CancellationToken cancellationToken);
    public Task<List<Comments>> GetComments(long entityId, CancellationToken cancellationToken);
    public Task ConfirmComment(int commentId, CancellationToken cancellationToken);
    public Task<List<Comments>> GetCommentsToAdmin(int entityId, CancellationToken cancellationToken);
}