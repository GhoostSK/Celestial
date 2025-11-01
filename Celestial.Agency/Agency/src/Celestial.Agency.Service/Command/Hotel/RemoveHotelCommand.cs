using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Hotel;

public sealed class RemoveHotelCommandReq : BaseEntity, IRequest<RemoveHotelCommandRes>
{
    public long Id { get; set; }
    public Guid ModifiedBy { get; set; }
}

public sealed class RemoveHotelCommandRes : ServiceResContextBase
{
}