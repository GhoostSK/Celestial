using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public sealed class ImagesRepository(MainDbContext dbContext) : RepositoryBase<Images>(dbContext), IImageService
{
    public async Task<List<Images>> GetImagesByEntityId(long entityId, string keyGroup,
        CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => p.EntityId == entityId && p.KeyGroup == keyGroup)
            .ToListAsync(cancellationToken);
    }
}