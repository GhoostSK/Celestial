using Celestial.Agency.Core.DAL.Model.Contact;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Contact;

public interface IContactUsService : IRepositoryEF<ContactUs>, IScopeLifetime
{
    Task<List<ContactUs>> GetContactList(CancellationToken cancellationToken);
}