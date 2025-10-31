using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Tour;

public class TourRequiredItemsRepository(MainDbContext dbContext)
    : RepositoryBase<TourEquipments>(dbContext), ITourEquipmentsService
{
    public async Task<List<TourEquipments>> GetListAsync(CancellationToken cancellationToken)
    {
        return await TableNoTracking.IgnoreQueryFilters().ToListAsync(cancellationToken);
    }
}