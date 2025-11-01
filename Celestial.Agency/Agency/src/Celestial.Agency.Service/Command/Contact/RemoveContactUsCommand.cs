using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Contact;

public sealed class RemoveContactUsCommandReq : BaseEntity, IRequest<RemoveContactUsCommandRes>
{
    public long ContactUsId { get; set; }
}

public sealed class RemoveContactUsCommandRes : ServiceResContextBase
{
}