using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

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