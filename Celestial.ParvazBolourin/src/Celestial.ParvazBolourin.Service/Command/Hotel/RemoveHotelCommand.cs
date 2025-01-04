using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Hotel;

public sealed class RemoveHotelCommandReq : BaseEntity, IRequest<RemoveHotelCommandRes>
{
    public int HotelId { get; set; }
}

public sealed class RemoveHotelCommandRes : ServiceResContextBase
{
}