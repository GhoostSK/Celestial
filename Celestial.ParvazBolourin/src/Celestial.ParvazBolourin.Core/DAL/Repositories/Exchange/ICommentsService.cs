using Celestial.ParvazBolourin.Core.DAL.Model.Exchange;
using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;

public interface ICurrencyService : IRepositoryEF<Currency>, IScopeLifetime
{
    public Task<List<Currency>> GetCurrencyList(CancellationToken cancellationToken);
}