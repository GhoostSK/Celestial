using Celestial.ParvazBolourin.Core.DAL.Model.Contact;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Contact;
using Celestial.ParvazBolourin.Service.Command.Contact;
using Mapster;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Contact;

public sealed class
    GetContactUsByIdCommandHandler : BaseRequestHandler<GetContactUsByIdCommandReq, GetContactUsByIdCommandRes>
{
    private readonly IContactUsService _contactUsRepository;

    public GetContactUsByIdCommandHandler(IContactUsService contactUsRepository)
    {
        _contactUsRepository = contactUsRepository;
    }

    protected override async Task<GetContactUsByIdCommandRes> HandleCore(GetContactUsByIdCommandReq request,
        CancellationToken cancellationToken)
    {
        var contactUs = request.Adapt<ContactUs>();

        var contactItem = await _contactUsRepository.FindByIdAsync(cancellationToken, request.Id);

        return new GetContactUsByIdCommandRes
        {
            Item = contactItem
        };
    }
}