using Celestial.Agency.Core.DAL.Model.Hotel;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Hotel;

public interface IHotelService : IRepositoryEF<Hotels>, IScopeLifetime
{
    Task<long> AddHotel(Hotels model, CancellationToken cancellationToken);

    public Task<(List<Hotels> Items, int TotalPages, int totalRecord)> GetHotelsList(GetHotelsListReq model,
        CancellationToken cancellationToken);

    public Task<Hotels?> GetHotelById(long id, CancellationToken cancellationToken);
}