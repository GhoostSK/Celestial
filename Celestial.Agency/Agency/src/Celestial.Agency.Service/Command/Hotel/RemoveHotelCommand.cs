using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Hotel;

public sealed class RemoveHotelCommandReq : BaseEntity, IRequest<RemoveHotelCommandRes>
{
    public long Id { get; set; }
    public Guid ModifiedBy { get; set; }
}

public sealed class RemoveHotelCommandRes : ServiceResContextBase
{
}