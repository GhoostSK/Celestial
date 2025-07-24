using Celestial.Agency.Core.DAL.Model.Contact;
using Celestial.Agency.Core.DAL.Repositories.Contact;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Contact;

public sealed class ContactUsRepository(MainDbContext dbContext)
    : RepositoryBase<ContactUs>(dbContext), IContactUsService
{
    public async Task<List<ContactUs>> GetContactList(CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => !p.IsDeleted).ToListAsync(cancellationToken);
    }
}