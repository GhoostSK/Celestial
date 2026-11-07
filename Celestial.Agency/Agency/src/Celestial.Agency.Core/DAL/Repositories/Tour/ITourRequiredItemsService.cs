using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Tour;

public interface ITourEquipmentsService : IRepositoryEF<TourEquipment>, IScopeLifetime
{
    public Task<List<TourEquipment>> GetListAsync(CancellationToken cancellationToken);
}