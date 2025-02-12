using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;

public class UpdateTourDto
{
    public int Id { get; set; }
    public Tours Tours { get; set; } = new();
    public List<TourLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
    public List<TourPlansUpdateDto> TourPlans { get; set; } = new();
}

public class TourPlansUpdateDto
{
    public int Id { get; set; } = 0;
    public List<string> PlanPicturePath { get; set; } = new();
    public List<Service.Command.Tour.TourPlanLocalizeDataDto> TourPlanLocalizeData { get; set; } = new();
}

public class TourPlanLocalizeDataDto
{
    public int LanguageId { get; set; }
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
}