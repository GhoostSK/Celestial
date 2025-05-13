using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;

public interface IHotelService : IRepositoryEF<Hotels>, IScopeLifetime
{
    Task<long> AddHotel(Hotels model, CancellationToken cancellationToken);
    Task<List<Hotels>> GetHotelsList(GetHotelsListReq model, CancellationToken cancellationToken);
    public Task<Hotels?> GetHotelById(long id, CancellationToken cancellationToken);

}