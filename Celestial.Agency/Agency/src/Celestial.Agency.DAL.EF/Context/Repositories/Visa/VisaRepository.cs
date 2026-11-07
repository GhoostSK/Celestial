using Celestial.Agency.Core.DAL.Model.Visa;
using Celestial.Agency.Core.DAL.Repositories.Visa;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Visa;

public class VisaRepository(MainDbContext dbContext) : RepositoryBase<Core.DAL.Model.Visa.Visa>(dbContext), IVisaService
{
    public async Task<int> AddVisa(Core.DAL.Model.Visa.Visa model, CancellationToken cancellationToken)
    {
        await AddAsync(model, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public async Task<List<Core.DAL.Model.Visa.Visa>> GetVisaList(int pageNumber = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = TableNoTracking.IgnoreQueryFilters();

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Core.DAL.Model.Visa.Visa?> GetVisaById(long visaId, CancellationToken cancellationToken)
    {
        return await Table
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == visaId, cancellationToken);
    }
}