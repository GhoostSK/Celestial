using Celestial.ParvazBolourin.Core.Attributes;
using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using System.ComponentModel.DataAnnotations;

namespace Celestial.ParvazBolourin.Api.Models.Dto;

public sealed class AddTourReqDto
{
    public ToursItemDto Tours { get; set; }
    public List<TourLocalizedPropertyItemDto> LocalizedProperty { get; set; }
}

public sealed class TourPlansItem
{
    public long TourId { get; set; }
    public List<string> PlanPictureId { get; set; } = new();
}

public sealed class TourLocalizedPropertyItemDto
{
    public int LanguageId { get; set; }
    public string Name { get; set; }
    public string TourMainText { get; set; }
    public List<string> TourFacilities { get; set; }
    public string TourTips { get; set; }
    public string PlanShortText { get; set; }
    public List<PlanTourItemDto> TourPlans { get; set; }
    [MaxLength(255)]
    public string MetaTitle { get; set; } = string.Empty;

    [MaxLength(500)]
    public string MetaDescription { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string MetaKeywords { get; set; } = string.Empty;
    [Slug]
    public string Slug { get; set; } = string.Empty;
}

public sealed class PlanTourItemDto
{
    public string PlanDayText { get; set; }
    public string PlanText { get; set; }
}

public sealed class GetToursListReqDto
{
    [Required] public int LanguageId { get; set; }
    public List<string> Countries { get; set; } = new();
    public List<string> Cities { get; set; } = new();
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public double ToPrice { get; set; }
    public double FromPrice { get; set; }
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();
    public bool IsActiveTour { get; set; }

}

public sealed class GetTourByIdReqDto
{
    [Required] public int LanguageId { get; set; }

    [Required] public int TourId { get; set; }
}

public sealed class RemoveTourByIdReqDto
{
    [Required] public int TourId { get; set; }
}
public class GetTourFeaturedRequestDto
{
    public int LanguageId { get; set; }
    public int TopCount { get; set; }
}