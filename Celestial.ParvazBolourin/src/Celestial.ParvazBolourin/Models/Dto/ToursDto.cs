using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;

namespace Celestial.ParvazBolourin.Api.Models.Dto;

public sealed class AddTourReqDto
{
    public TourItemDto Tours { get; set; }
    public List<TourLocalizedPropertyItemDto> LocalizedProperty { get; set; }
}

public sealed class TourItemDto
{
    public List<int> CountriesId { get; set; } = new();
    public List<int> StatesId { get; set; } = new();
    public List<long> CitiesId { get; set; } = new();
    public string HotelCode { get; set; } = string.Empty;
    public TourStatus TourStatus { get; set; }
    public DateTime StartDate { get; set; }
    public int PassengerCapacity { get; set; }
    public string ImportantTips { get; set; } = string.Empty;
    public List<TourPlans> TourPlans { get; set; } = new();
    public List<TourTransPorts> TransPorts { get; set; } = new();
    public List<int> Hotels { get; set; } = new();
    public TourPrimaryTransport PrimaryTransport { get; set; } = new();
    public int Price { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public int Score { get; set; }
    public Continent TourContinent { get; set; }
    public TourType TourType { get; set; }
    public TourScope TourScope { get; set; }
    public int DayCount { get; set; }
    public bool RequiresVisa { get; set; }
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
    public long ToPrice { get; set; }
    public long FromPrice { get; set; }
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