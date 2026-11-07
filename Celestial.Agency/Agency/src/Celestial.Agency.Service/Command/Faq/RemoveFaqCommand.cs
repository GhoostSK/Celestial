using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Faq;

public sealed class RemoveFaqCommandReq : BaseEntity, IRequest<RemoveFaqCommandRes>
{
    public long Id { get; set; }
}

public sealed class RemoveFaqCommandRes : ServiceResContextBase
{
}
