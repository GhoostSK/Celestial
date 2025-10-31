using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public sealed class LanguageRepository(MainDbContext dbContext) : RepositoryBase<Language>(dbContext), ILanguageService
{
    public async Task<List<Language>> GetLanguages(CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .ToListAsync(cancellationToken);
    }
}