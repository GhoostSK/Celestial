using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public sealed class LanguageRepository(MainDbContext dbContext) : RepositoryBase<Language>(dbContext), ILanguageService
{
    public async Task<List<Language>> GetLanguages(CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .ToListAsync(cancellationToken);
    }
}