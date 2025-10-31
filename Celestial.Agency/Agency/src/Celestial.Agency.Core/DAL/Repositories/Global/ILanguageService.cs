using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Global;

public interface ILanguageService : IRepositoryEF<Language>, IScopeLifetime
{
    Task<List<Language>> GetLanguages(CancellationToken cancellationToken);
}