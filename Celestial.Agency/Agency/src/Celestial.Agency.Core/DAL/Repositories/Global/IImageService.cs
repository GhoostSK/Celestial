using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Global;

public interface IImageService : IRepositoryEF<Image>, IScopeLifetime
{
    public Task<List<Image>> GetImagesByEntityId(long entityId, string keyGroup, CancellationToken cancellationToken);
}