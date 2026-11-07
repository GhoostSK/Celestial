using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public sealed class ImagesRepository(MainDbContext dbContext) : RepositoryBase<Image>(dbContext), IImageService
{
    public async Task<List<Image>> GetImagesByEntityId(long entityId, string keyGroup,
        CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .IgnoreQueryFilters()
            .Where(p => p.EntityId == entityId && p.KeyGroup == keyGroup)
            .ToListAsync(cancellationToken);
    }
}