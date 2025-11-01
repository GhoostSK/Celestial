using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Tour;

public sealed class RemoveTourCommandReq : BaseEntity, IRequest<RemoveTourCommandRes>
{
    public int Id { get; set; }
}

public sealed class RemoveTourCommandRes : ServiceResContextBase
{
}