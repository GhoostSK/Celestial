using Celestial.Agency.Core.DAL.Repositories.Contact;
using Celestial.Agency.Service.Command.Contact;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Contact;

public sealed class
    RemoveContactUsCommandHandler : BaseRequestHandler<RemoveContactUsCommandReq, RemoveContactUsCommandRes>
{
    private readonly IContactUsService _contactUsRepository;

    public RemoveContactUsCommandHandler(IContactUsService contactUsRepository)
    {
        _contactUsRepository = contactUsRepository;
    }

    protected override async Task<RemoveContactUsCommandRes> HandleCore(RemoveContactUsCommandReq request,
        CancellationToken cancellationToken)
    {
        var contactUs = await _contactUsRepository.FindByIdAsync(cancellationToken, request.ContactUsId);

        if (contactUs == null)
            return new RemoveContactUsCommandRes
            {
                Error = new ValidationError("CU404", "ContactUs not found.")
            };

        contactUs.IsDeleted = true;
        await _contactUsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new RemoveContactUsCommandRes();
    }
}