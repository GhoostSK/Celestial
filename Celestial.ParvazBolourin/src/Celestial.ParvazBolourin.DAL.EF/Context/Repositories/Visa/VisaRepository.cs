using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Visa;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Visa;

public class VisaRepository(MainDbContext dbContext) : RepositoryBase<Visas>(dbContext), IVisaService
{
    public async Task<int> AddVisa(Visas model, CancellationToken cancellationToken)
    {
        await AddAsync(model, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public async Task<List<Visas>> GetVisaList(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var query = TableNoTracking.Where(p => !p.IsDeleted);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Visas?> GetVisaById(long visaId, CancellationToken cancellationToken)
    {
        return await Table.FirstOrDefaultAsync(p => p.Id == visaId && p.IsDeleted == false, cancellationToken);
    }
}