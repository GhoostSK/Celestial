using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Hotel;

public interface IHotelService : IRepositoryEF<DAL.Model.Hotel.Hotel>, IScopeLifetime
{
    Task<long> AddHotel(DAL.Model.Hotel.Hotel model, CancellationToken cancellationToken);

    public Task<(List<DAL.Model.Hotel.Hotel> Items, int TotalPages, int totalRecord)> GetHotelsList(GetHotelsListReq model,
        CancellationToken cancellationToken);

    public Task<DAL.Model.Hotel.Hotel?> GetHotelById(long id, CancellationToken cancellationToken);
}