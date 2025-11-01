using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Tour.TourEquipment;

public sealed class GetTourEquipmentByIdCommandReq : BaseEntity, IRequest<GetTourEquipmentByIdCommandRes>
{
    public long Id { get; set; }
    public string Language { get; set; } = string.Empty;
}

public sealed class GetTourEquipmentByIdCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}