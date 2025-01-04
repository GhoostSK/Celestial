using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Global;

public interface ILocalizedPropertyService : IRepositoryEF<LocalizedProperty>, IScopeLifetime
{
    public Task<List<GetLocalizedPropertyRes>> GetPropertyByLanguageAndEntityId(GetLocalizedPropertyReq model,
        CancellationToken cancellationToken);
}