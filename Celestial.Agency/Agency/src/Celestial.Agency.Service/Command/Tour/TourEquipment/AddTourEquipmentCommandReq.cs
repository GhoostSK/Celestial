using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Tour.TourEquipment;

public sealed class AddTourEquipmentCommandReq : BaseEntity, IRequest<AddTourEquipmentCommandRes>
{
    public List<LocalizedTourEquipmentNameDto> LocalizedNames { get; set; } = new();
    public string Icon { get; set; } = string.Empty;
}

public sealed class AddTourEquipmentCommandRes : ServiceResContextBase
{
}

public sealed class LocalizedTourEquipmentNameDto
{
    public string Language { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}