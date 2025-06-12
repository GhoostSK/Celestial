using Celestial.ParvazBolourin.Core.DAL.Model.Tour.TourEquipment;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Tour;

public class TourRequiredItemsRepository(MainDbContext dbContext) : RepositoryBase<TourEquipments>(dbContext), ITourEquipmentsService
{
    public async Task<List<TourEquipments>> GetListAsync(CancellationToken cancellationToken)
    {
        return await TableNoTracking.IgnoreQueryFilters().Where(p => !p.IsDeleted).ToListAsync(cancellationToken: cancellationToken);
    }
}

