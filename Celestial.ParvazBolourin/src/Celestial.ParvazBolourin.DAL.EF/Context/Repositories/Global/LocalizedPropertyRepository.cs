using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Global;

public sealed class LocalizedPropertyRepository(MainDbContext dbContext)
    : RepositoryBase<LocalizedProperty>(dbContext), ILocalizedPropertyService
{
    public async Task<List<GetLocalizedPropertyRes>> GetPropertyByLanguageAndEntityId(GetLocalizedPropertyReq model,
        CancellationToken cancellationToken)
    {
        return await Table
            .Where(p => p.LanguageId == model.LanguageId && p.EntityId == model.EntityId &&
                        p.LocalKeyGroup == model.LocalKeyGroup)
            .Select(p => new GetLocalizedPropertyRes
            {
                Id = p.Id,
                LocalKey = p.LocalKey,
                LocalValue = p.LocalValue,
                KeyGroup = p.LocalKeyGroup,
                LanguageId = p.LanguageId
            })
            .ToListAsync(cancellationToken);
    }
}