using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Hotel;

public interface IHotelService : IRepositoryEF<Hotels>, IScopeLifetime
{
    Task<long> AddHotel(Hotels model, CancellationToken cancellationToken);

    public Task<(List<Hotels> Items, int TotalPages, int totalRecord)> GetHotelsList(GetHotelsListReq model,
        CancellationToken cancellationToken);

    public Task<Hotels?> GetHotelById(long id, CancellationToken cancellationToken);
}