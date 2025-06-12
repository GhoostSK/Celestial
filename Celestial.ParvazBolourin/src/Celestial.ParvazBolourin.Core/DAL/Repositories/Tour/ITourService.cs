using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;

public interface ITourService : IRepositoryEF<Tours>, IScopeLifetime
{
    public Task<(List<Tours> Items, int TotalPages, int totalRecord)> GetToursList(GetTourListReq model, CancellationToken cancellationToken);

    public Task<Tours?> GetTourById(long tourId, CancellationToken cancellationToken);
    public Task<List<Tours>> GetTopToursByViews(int topCount, CancellationToken cancellationToken);
    public Task<string> GenerateUniqueTourCodeAsync(CancellationToken cancellationToken);
}