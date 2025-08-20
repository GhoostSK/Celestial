using Celestial.Agency.Core.DAL.Model.Exchange;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Exchange;

public interface ICurrencyService : IRepositoryEF<Currency>, IScopeLifetime
{
    public Task<List<Currency>> GetCurrencyList(CancellationToken cancellationToken);
}