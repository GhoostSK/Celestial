using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Global;

public interface ILocalizedPropertyService : IRepositoryEF<LocalizedProperty>, IScopeLifetime
{
    public Task<List<GetLocalizedPropertyRes>> GetPropertyByLanguageAndEntityId(GetLocalizedPropertyReq model,
        CancellationToken cancellationToken);
}