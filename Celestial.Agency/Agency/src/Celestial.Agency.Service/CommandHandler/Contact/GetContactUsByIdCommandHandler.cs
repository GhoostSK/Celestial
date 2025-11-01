using Celestial.Agency.Core.DAL.Model.Contact;
using Celestial.Agency.Core.DAL.Repositories.Contact;
using Celestial.Agency.Service.Command.Contact;
using Celestial.SharedKernel.Mediator;
using Mapster;

namespace Celestial.Agency.Service.CommandHandler.Contact;

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