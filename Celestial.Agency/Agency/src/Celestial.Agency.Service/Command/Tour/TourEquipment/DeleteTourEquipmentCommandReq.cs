using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Tour.TourEquipment;

public sealed class DeleteTourEquipmentCommandReq : BaseEntity, IRequest<DeleteTourEquipmentCommandRes>
{
    public long Id { get; set; }
}

public sealed class DeleteTourEquipmentCommandRes : ServiceResContextBase
{
}