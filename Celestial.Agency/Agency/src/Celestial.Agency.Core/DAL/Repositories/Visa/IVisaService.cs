using Celestial.Agency.Core.DAL.Model.Visa;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Visa;

public interface IVisaService : IRepositoryEF<Visas>, IScopeLifetime
{
    public Task<int> AddVisa(Visas model, CancellationToken cancellationToken);

    public Task<List<Visas>> GetVisaList(int pageNumber = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);

    public Task<Visas?> GetVisaById(long tourId, CancellationToken cancellationToken);
}