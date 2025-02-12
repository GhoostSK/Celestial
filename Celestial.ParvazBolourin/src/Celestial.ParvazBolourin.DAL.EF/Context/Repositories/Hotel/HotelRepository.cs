using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Hotel;

public sealed class HotelRepository(MainDbContext dbContext) : RepositoryBase<Hotels>(dbContext), IHotelService
{
    public async Task<int> AddHotel(Hotels model, CancellationToken cancellationToken)
    {
        await AddAsync(model, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public async Task<List<Hotels>> GetHotelsList(GetHotelsListReq model, CancellationToken cancellationToken)
    {
        var query = Table
            .IgnoreQueryFilters()
            .Where(p => !p.IsDeleted);

        if (!string.IsNullOrEmpty(model.Name))
            query = query.Where(p => p.Name.Contains(model.Name));

        if (model.FromPrice != 0 && model.ToPrice != 0)
            query = query.Where(p => p.Price >= model.FromPrice && p.Price <= model.ToPrice);

        if (model.HotelContinent.Any())
            query = query.Where(p => model.HotelContinent.Contains(p.HotelContinent));

        if (model.HotelLocation.Any())
            query = query.Where(p => model.HotelLocation.Contains(p.HotelLocation));

        if (model.HotelCountry.Any()) query = query.Where(p => model.HotelCountry.Contains(p.HotelCountry));


        if (model.HotelScore.Any())
            query = query.Where(p => model.HotelScore.Contains(p.HotelScore));

        return await query
            .Include(p => p.HotelPictures)
            .ToListAsync(cancellationToken);
    }
}