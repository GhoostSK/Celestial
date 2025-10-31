using Celestial.Agency.Core.DAL.Model.Exchange;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Exchange;

public interface ICurrencyService : IRepositoryEF<Currency>, IScopeLifetime
{
    public Task<List<Currency>> GetCurrencyList(CancellationToken cancellationToken);
}