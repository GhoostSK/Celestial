using Celestial.Agency.Api.Identity.Services.UserIdModelBinder;
using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Agency.Service.Command.Tour;

namespace Celestial.Agency.Api.Areas.Admin.Models.Dto;

public class UpdateTourDto
{
    public int Id { get; set; }
    public Tours Tours { get; set; } = new();
    public List<TourLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
    public List<TourPlansUpdateDto> TourPlans { get; set; } = new();

    [FromTokenUserId] public Guid CreatedBy { get; set; }
}

public class TourPlansUpdateDto
{
    public int Id { get; set; } = 0;
    public List<string> PlanPicturePath { get; set; } = new();
    public List<Service.Command.Tour.TourPlanLocalizeDataDto> TourPlanLocalizeData { get; set; } = new();
}

public class TourPlanLocalizeDataDto
{
    public string Language { get; set; }
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
}

public sealed class AddTourEquipmentDto
{
    public List<LocalizedTourEquipmentDto> LocalizedNames { get; set; } = new();
    public string Icon { get; set; } = string.Empty;
}

public sealed class LocalizedTourEquipmentDto
{
    public string Language { get; set; }
    public string Name { get; set; } = string.Empty;
}