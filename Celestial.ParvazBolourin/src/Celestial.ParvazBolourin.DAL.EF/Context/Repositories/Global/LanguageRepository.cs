using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Global;

public sealed class LanguageRepository(MainDbContext dbContext) : RepositoryBase<Language>(dbContext), ILanguageService
{
    public async Task<List<Language>> GetLanguages(CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .ToListAsync(cancellationToken);
    }
}