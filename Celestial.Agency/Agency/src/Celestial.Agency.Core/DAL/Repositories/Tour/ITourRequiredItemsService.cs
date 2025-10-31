using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Tour;

public interface ITourEquipmentsService : IRepositoryEF<TourEquipments>, IScopeLifetime
{
    public Task<List<TourEquipments>> GetListAsync(CancellationToken cancellationToken);
}