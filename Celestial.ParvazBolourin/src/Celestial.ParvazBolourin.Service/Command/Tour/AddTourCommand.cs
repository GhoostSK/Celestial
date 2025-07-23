using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.Attributes;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.Command.Tour;

public sealed class AddTourCommandReq : BaseEntity, IRequest<AddTourCommandRes>
{
    public ToursItemDto Tours { get; set; } = new();
    public List<TourLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
}

public sealed class AddTourCommandRes : ServiceResContextBase
{
}

public sealed class ToursItemDto
{
    public long Id { get; set; }
    public long ImageId { get; set; }
    public List<int> CountriesId { get; set; } = new();
    public List<int> CitiesId { get; set; } = new();
    public string TourCode { get; set; } = string.Empty;
    public TourStatus TourStatus { get; set; }
    public DateOnly StartDate { get; set; }
    public int PassengerCapacity { get; set; }
    public string ImportantTips { get; set; } = string.Empty;
    public List<TourPlansItem> TourPlans { get; set; } = new();
    public List<TourTransPorts> TransPorts { get; set; } = new();
    public List<long> UnNecessaryEquipment { get; set; } = new();
    public List<long> NecessaryEquipment { get; set; } = new();
    public List<long> Hotels { get; set; } = new();
    public TourPrimaryTransport PrimaryTransport { get; set; } = new();
    public double Price { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public int Score { get; set; }
    public Continent TourContinent { get; set; }
    public TourType TourType { get; set; }
    public TourScope TourScope { get; set; }
    public int DayCount { get; set; }
    public bool RequiresVisa { get; set; }
    public DateOnly RegistrationDeadline { get; set; }

    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; } = string.Empty;
    public List<DateOnly> ExecDate { get; set; } = new();
    [MaxLength(255)]
    public string MetaTitle { get; set; } = string.Empty;

    [MaxLength(500)]
    public string MetaDescription { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string MetaKeywords { get; set; } = string.Empty;
    [Slug]
    public string Slug { get; set; } = string.Empty;
}
public sealed class TourPlansItem
{
    public long TourId { get; set; }
    public List<string> PlanPicturePath { get; set; } = new();
}

public sealed class TourLocalizedPropertyItem
{
    public string Language { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string TourMainText { get; set; } = string.Empty;
    public List<string> TourFacilities { get; set; } = new();
    public string TourTips { get; set; } = string.Empty;
    public string PlanShortText { get; set; } = string.Empty;
    public List<PlanTourItem> TourPlans { get; set; } = new();
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class PlanTourItem
{
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
}