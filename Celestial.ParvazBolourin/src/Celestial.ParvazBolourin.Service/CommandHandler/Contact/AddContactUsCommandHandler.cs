using Celestial.ParvazBolourin.Core.DAL.Model.Contact;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Contact;
using Celestial.ParvazBolourin.Service.Command.Contact;
using Mapster;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.Service.CommandHandler.Contact;

public sealed class AddContactUsCommandHandler(IContactUsService contactUsRepository)
    : BaseRequestHandler<AddContactUsCommandReq, AddContactUsCommandRes>
{
    protected override async Task<AddContactUsCommandRes> HandleCore(AddContactUsCommandReq request,
        CancellationToken cancellationToken)
    {
        var contactUs = request.Adapt<ContactUs>();

        await contactUsRepository.AddAsync(contactUs, cancellationToken);
        await contactUsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddContactUsCommandRes();
    }
}