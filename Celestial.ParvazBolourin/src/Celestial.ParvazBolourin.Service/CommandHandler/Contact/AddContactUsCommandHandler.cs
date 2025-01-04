using Celestial.ParvazBolourin.Core.DAL.Model.Contact;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Contact;
using Celestial.ParvazBolourin.Service.Command.Contact;
using Mapster;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Contact;

public sealed class AddContactUsCommandHandler : BaseRequestHandler<AddContactUsCommandReq, AddContactUsCommandRes>
{
    private readonly IContactUsService _contactUsRepository;

    public AddContactUsCommandHandler(IContactUsService contactUsRepository)
    {
        _contactUsRepository = contactUsRepository;
    }

    protected override async Task<AddContactUsCommandRes> HandleCore(AddContactUsCommandReq request, CancellationToken cancellationToken)
    {
        var contactUs = request.Adapt<ContactUs>();

        await _contactUsRepository.AddAsync(contactUs, cancellationToken);
        await _contactUsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddContactUsCommandRes();
    }
}