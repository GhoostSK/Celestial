using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Contact;

public sealed class RemoveContactUsCommandReq : BaseEntity, IRequest<RemoveContactUsCommandRes>
{
    public long ContactUsId { get; set; }
}

public sealed class RemoveContactUsCommandRes : ServiceResContextBase
{
}