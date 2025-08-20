using Celestial.Agency.Core.DAL.Model.Global;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Global;

public interface ILanguageService : IRepositoryEF<Language>, IScopeLifetime
{
    Task<List<Language>> GetLanguages(CancellationToken cancellationToken);
}