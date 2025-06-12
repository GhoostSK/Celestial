using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour.TourEquipment;

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
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
}
