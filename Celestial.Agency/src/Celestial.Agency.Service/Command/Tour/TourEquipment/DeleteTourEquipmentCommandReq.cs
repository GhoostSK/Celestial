using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Tour.TourEquipment;

public sealed class DeleteTourEquipmentCommandReq : BaseEntity, IRequest<DeleteTourEquipmentCommandRes>
{
    public long Id { get; set; }
}

public sealed class DeleteTourEquipmentCommandRes : ServiceResContextBase
{
}