using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour;

public sealed class UpdateTourCommandReq : BaseEntity, IRequest<UpdateTourCommandRes>
{
    public int Id { get; set; }
    public Tours Tours { get; set; } = new();
    public List<TourLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
    public List<TourPlansUpdateReq> TourPlans { get; set; } = new();
}

public sealed class UpdateTourCommandRes : ServiceResContextBase
{
}

public class TourPlansUpdateReq
{
    public int Id { get; set; } = 0;
    public List<string> PlanPicturePath { get; set; } = new();
    public List<TourPlanLocalizeDataDto> TourPlanLocalizeData { get; set; } = new();
}

public class TourPlanLocalizeDataDto
{
    public int LanguageId { get; set; }
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
}