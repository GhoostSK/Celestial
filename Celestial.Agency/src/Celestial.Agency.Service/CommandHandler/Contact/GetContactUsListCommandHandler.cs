using Celestial.Agency.Core.DAL.Repositories.Contact;
using Celestial.Agency.Service.Command.Contact;
using Mapster;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.Agency.Service.CommandHandler.Contact;

public sealed class
    GetContactUsListCommandHandler : BaseRequestHandler<GetContactUsListCommandReq, GetContactUsListCommandRes>
{
    private readonly IContactUsService _contactUsRepository;

    public GetContactUsListCommandHandler(IContactUsService contactUsRepository)
    {
        _contactUsRepository = contactUsRepository;
    }

    protected override async Task<GetContactUsListCommandRes> HandleCore(GetContactUsListCommandReq request,
        CancellationToken cancellationToken)
    {
        var contactUsList = await _contactUsRepository.GetContactList(cancellationToken);

        return new GetContactUsListCommandRes
        {
            ContactUsList = contactUsList.Adapt<List<ContactUsDto>>()
        };
    }
}