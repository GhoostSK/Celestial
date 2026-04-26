using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Api.Identity.Services.UserIdModelBinder;
using Celestial.Agency.Core.Attributes;
using Celestial.Agency.Core.Enum.Global;
using Celestial.Agency.Core.Enum.Tour;
using Celestial.Agency.Service.Command.Tour;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Models.Dto;

public sealed class AddTourReqDto
{
    public ToursItemDto Tours { get; set; }
    public List<TourLocalizedPropertyItemDto> LocalizedProperty { get; set; }

    [FromTokenUserId] public Guid CreatedBy { get; set; }
}

public sealed class TourPlansItem
{
    public long TourId { get; set; }
    public List<string> PlanPictureId { get; set; } = new();
}

public sealed class TourLocalizedPropertyItemDto
{
    public string Language { get; set; }
    public string Name { get; set; }
    public string TourMainText { get; set; }
    public List<string> TourFacilities { get; set; }
    public string TourTips { get; set; }
    public string PlanShortText { get; set; }
    public List<PlanTourItemDto> TourPlans { get; set; }

    [MaxLength(255)] public string MetaTitle { get; set; } = string.Empty;

    [MaxLength(500)] public string MetaDescription { get; set; } = string.Empty;

    [MaxLength(1000)] public string MetaKeywords { get; set; } = string.Empty;

    [Slug] public string Slug { get; set; } = string.Empty;
}

public sealed class PlanTourItemDto
{
    public string PlanDayText { get; set; }
    public string PlanText { get; set; }
}

public sealed class GetToursListReqDto
{
    [Required] [FromRoute] public string Language { get; set; }

    public string Title { get; set; }
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
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public sealed class GetEquipmentById
{
    public long Id { get; set; }
    public string Language { get; set; }
}

public sealed class GetTourByIdReqDto
{
    [FromRoute] public string Language { get; set; }

    [Required] public int TourId { get; set; }

    [Slug] [FromRoute] public string Slug { get; set; } = string.Empty;
}

public sealed class RemoveTourByIdReqDto
{
    [Required] public int Id { get; set; }

    [FromTokenUserId] public Guid CreatedBy { get; set; }
}

public class GetTourFeaturedRequestDto
{
    [FromRoute] public string Language { get; set; }

    public int TopCount { get; set; }
}

public sealed class RemoveTourRequiredItemByIdReqDto
{
    [Required] public int TourId { get; set; }
}

public sealed class GetTourByIdRequiredItemReqDto
{
    public long Id { get; set; }
    public string Language { get; set; }
}