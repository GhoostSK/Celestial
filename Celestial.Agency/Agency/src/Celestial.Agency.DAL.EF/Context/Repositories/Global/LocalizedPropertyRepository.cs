using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public sealed class LocalizedPropertyRepository(MainDbContext dbContext)
    : RepositoryBase<LocalizedProperty>(dbContext), ILocalizedPropertyService
{
    public async Task<List<GetLocalizedPropertyRes>> GetPropertyByLanguageAndEntityId(GetLocalizedPropertyReq model,
        CancellationToken cancellationToken)
    {
        return await Table
            .Where(p => p.Language == model.Language && p.EntityId == model.EntityId &&
                        p.LocalKeyGroup == model.LocalKeyGroup)
            .Select(p => new GetLocalizedPropertyRes
            {
                Id = p.Id,
                LocalKey = p.LocalKey,
                LocalValue = p.LocalValue,
                LocalKeyGroup = p.LocalKeyGroup,
                Language = p.Language
            })
            .ToListAsync(cancellationToken);
    }
}