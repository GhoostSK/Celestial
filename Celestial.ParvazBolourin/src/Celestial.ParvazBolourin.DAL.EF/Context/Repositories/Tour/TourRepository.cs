using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Tour;

public class TourRepository(MainDbContext dbContext) : RepositoryBase<Tours>(dbContext), ITourService
{
    public async Task<List<Tours>> GetToursList(GetTourListReq model, CancellationToken cancellationToken)
    {
        var query = TableNoTracking
            .Where(p => !p.IsDeleted);

        if (model.Countries.Any()) query = query.Where(p => p.CountriesId.Any(c => model.Countries.Contains(c)));

        if (model.Cities.Any()) query = query.Where(p => p.CitiesId.Any(c => model.Cities.Contains(c)));

        if (model.FromPrice != 0 && model.ToPrice != 0)
            query = query.Where(p => p.Price >= model.FromPrice && p.Price <= model.ToPrice);

        if (model.FromDay != 0 && model.ToDay != 0)
            query = query.Where(p => p.DayCount >= model.FromDay && p.DayCount <= model.ToDay);

        if (model.TourContinent.Any()) query = query.Where(p => model.TourContinent.Contains(p.TourContinent));

        if (model.TourType.Any()) query = query.Where(p => model.TourType.Contains(p.TourType));

        if (model.TourScope.Any()) query = query.Where(p => model.TourScope.Contains(p.TourScope));

        if (model.IsActiveTour) query = query.Where(p => p.TourStatus == TourStatus.Active);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Tours?> GetTourById(long tourId, CancellationToken cancellationToken)
    {
        return await Table
            .IgnoreQueryFilters()
            .Include(p => p.PrimaryTransport)
            .Include(p => p.TransPorts)
            .Include(p => p.TourPlans)
            .FirstOrDefaultAsync(p => p.Id == tourId && !p.IsDeleted, cancellationToken);
    }
}