using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour.TourEquipment;

public sealed class GetTourEquipmentListCommandReq : BaseEntity, IRequest<GetTourEquipmentListCommandRes>
{
    public int LanguageId { get; set; }
}

public sealed class GetTourEquipmentListCommandRes : ServiceResContextBase
{
    public List<TourEquipmentListItemDto> Items { get; set; } = new();
}

public sealed class TourEquipmentListItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; }
}