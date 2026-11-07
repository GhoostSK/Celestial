using Celestial.Agency.Core.DAL.Model.Visa;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Visa;

public interface IVisaService : IRepositoryEF<Model.Visa.Visa>, IScopeLifetime
{
    public Task<int> AddVisa(Model.Visa.Visa model, CancellationToken cancellationToken);

    public Task<List<Model.Visa.Visa>> GetVisaList(int pageNumber = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);

    public Task<Model.Visa.Visa?> GetVisaById(long tourId, CancellationToken cancellationToken);
}