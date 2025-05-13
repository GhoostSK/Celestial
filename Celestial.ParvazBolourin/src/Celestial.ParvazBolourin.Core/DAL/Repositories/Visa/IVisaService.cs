using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Visa;

public interface IVisaService : IRepositoryEF<Visas>, IScopeLifetime
{
    public Task<int> AddVisa(Visas model, CancellationToken cancellationToken);

    public Task<List<Visas>> GetVisaList(CancellationToken cancellationToken);
    public Task<Visas?> GetVisaById(long tourId, CancellationToken cancellationToken);
}