using Celestial.Agency.Core.DAL.Model.Contact;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Contact;

public interface IContactUsService : IRepositoryEF<ContactUs>, IScopeLifetime
{
    Task<List<ContactUs>> GetContactList(CancellationToken cancellationToken);
}