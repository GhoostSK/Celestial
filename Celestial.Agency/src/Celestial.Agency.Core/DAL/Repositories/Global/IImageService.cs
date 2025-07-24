using Celestial.Agency.Core.DAL.Model.Global;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Global;

public interface IImageService : IRepositoryEF<Images>, IScopeLifetime
{
    public Task<List<Images>> GetImagesByEntityId(long entityId, string keyGroup, CancellationToken cancellationToken);
}