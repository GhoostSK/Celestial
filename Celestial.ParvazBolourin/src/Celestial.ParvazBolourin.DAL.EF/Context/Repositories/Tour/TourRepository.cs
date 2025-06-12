using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Tour;

public class TourRepository(MainDbContext dbContext) : RepositoryBase<Tours>(dbContext), ITourService
{
    public async Task<(List<Tours> Items, int TotalPages, int totalRecord)> GetToursList(GetTourListReq model, CancellationToken cancellationToken)
    {
        var query = TableNoTracking
            .Where(p => !p.IsDeleted);

        if (model.Countries.Any())
            query = query.Where(p => p.CountriesId.Any(c => model.Countries.Contains(c)));

        if (model.Cities.Any())
            query = query.Where(p => p.CitiesId.Any(c => model.Cities.Contains(c)));

        if (model.FromPrice != 0 && model.ToPrice != 0)
            query = query.Where(p => p.BasePrice >= model.FromPrice && p.BasePrice <= model.ToPrice);

        if (model.FromDay != 0 && model.ToDay != 0)
            query = query.Where(p => p.ExecDate.Count >= model.FromDay && p.ExecDate.Count <= model.ToDay);

        if (model.TourContinent.Any())
            query = query.Where(p => model.TourContinent.Contains(p.TourContinent));

        if (model.TourType.Any())
            query = query.Where(p => model.TourType.Contains(p.TourType));

        if (model.TourScope.Any())
            query = query.Where(p => model.TourScope.Contains(p.TourScope));

        if (model.IsActiveTour)
            query = query.Where(p => p.TourStatus == TourStatus.Active);

        var totalItems = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalItems / (double)model.PageSize);

        var items = await query
            .Skip((model.PageNumber - 1) * model.PageSize)
            .Take(model.PageSize)
            .ToListAsync(cancellationToken);
        int totalRecord = await Table.IgnoreQueryFilters().CountAsync(cancellationToken);

        return (items, totalPages, totalRecord);
    }




    public async Task<Tours?> GetTourById(long tourId, CancellationToken cancellationToken)
    {
        var tour = await Table
            .IgnoreQueryFilters()
            .Include(p => p.PrimaryTransport)
            .Include(p => p.TransPorts)
            .Include(p => p.TourPlans)
            .FirstOrDefaultAsync(p => p.Id == tourId && !p.IsDeleted, cancellationToken);

        if (tour != null)
        {
            tour.Views++;
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return tour;
    }

    public async Task<List<Tours>> GetTopToursByViews(int topCount, CancellationToken cancellationToken)
    {
        var query = TableNoTracking
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.Views);

        var topTours = await query.Take(topCount).ToListAsync(cancellationToken);

        if (topTours.Count < topCount)
        {
            topTours = await query.ToListAsync(cancellationToken);
        }

        return topTours;
    }

    public string GenerateTourCode(CancellationToken cancellationToken)
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";

        var random = new Random();
        var part1 = new string(Enumerable.Range(0, 3).Select(_ => digits[random.Next(digits.Length)]).ToArray());
        var part2 = new string(Enumerable.Range(0, 3).Select(_ => upper[random.Next(upper.Length)]).ToArray());
        var part3 = new string(Enumerable.Range(0, 2).Select(_ => digits[random.Next(digits.Length)]).ToArray());

        return $"{part1}{part2}{part3}";
    }

    public async Task<string> GenerateUniqueTourCodeAsync(CancellationToken cancellationToken)
    {
        string code;
        do
        {
            code = GenerateTourCode(cancellationToken);
        } while (await Table.AnyAsync(t => t.TourCode == code, cancellationToken));

        return code;
    }
}