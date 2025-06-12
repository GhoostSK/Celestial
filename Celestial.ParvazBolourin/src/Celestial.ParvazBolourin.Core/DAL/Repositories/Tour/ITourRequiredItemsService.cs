using Celestial.ParvazBolourin.Core.DAL.Model.Tour.TourEquipment;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;

public interface ITourEquipmentsService : IRepositoryEF<TourEquipments>, IScopeLifetime
{
    public Task<List<TourEquipments>> GetListAsync(CancellationToken cancellationToken);


}