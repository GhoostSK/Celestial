using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Tour;

public interface ITourService : IRepositoryEF<Model.Tour.Tour>, IScopeLifetime
{
    public Task<(List<Model.Tour.Tour> Items, int TotalPages, int totalRecord)> GetToursList(GetTourListReq model,
        CancellationToken cancellationToken);

    public Task<Model.Tour.Tour?> GetTourById(long tourId, CancellationToken cancellationToken);

    public Task<List<Model.Tour.Tour>> GetTopToursByViews(int topCount, CancellationToken cancellationToken);

    public Task<string> GenerateUniqueTourCodeAsync(CancellationToken cancellationToken);
}