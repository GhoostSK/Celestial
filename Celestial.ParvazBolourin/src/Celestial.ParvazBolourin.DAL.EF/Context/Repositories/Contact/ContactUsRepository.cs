using Celestial.ParvazBolourin.Core.DAL.Model.Contact;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Contact;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Contact;

public class ContactUsRepository(MainDbContext dbContext) : RepositoryBase<ContactUs>(dbContext), IContactUsService
{
    public async Task<List<ContactUs>> GetContactList( CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => !p.IsDeleted).ToListAsync(cancellationToken: cancellationToken);
    }
}