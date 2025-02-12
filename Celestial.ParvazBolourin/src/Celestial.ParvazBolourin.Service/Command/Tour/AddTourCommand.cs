using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour;

public sealed class AddTourCommandReq : BaseEntity, IRequest<AddTourCommandRes>
{
    public Tours Tours { get; set; } = new();
    public List<TourLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
}

public sealed class AddTourCommandRes : ServiceResContextBase
{
}

public sealed class TourLocalizedPropertyItem
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TourMainText { get; set; } = string.Empty;
    public List<string> TourFacilities { get; set; } = new();
    public string TourTips { get; set; } = string.Empty;
    public string PlanShortText { get; set; } = string.Empty;
    public List<PlanTourItem> TourPlans { get; set; } = new();
}

public sealed class PlanTourItem
{
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
}