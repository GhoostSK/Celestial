using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Tour.TourEquipment;

public sealed class GetTourEquipmentListCommandReq : BaseEntity, IRequest<GetTourEquipmentListCommandRes>
{
    public string Language { get; set; } = string.Empty;
}

public sealed class GetTourEquipmentListCommandRes : ServiceResContextBase
{
    public List<TourEquipmentListItemDto> Items { get; set; } = new();
}

public sealed class TourEquipmentListItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}