using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.Attributes;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Tour;

public sealed class Tours : DbBaseEntity<long>
{
    public List<int> CountriesId { get; set; } = new();
    public List<int> CitiesId { get; set; } = new();
    public string HotelCode { get; set; } = string.Empty;
    public TourStatus TourStatus { get; set; }
    public DateTime StartDate { get; set; }
    public int PassengerCapacity { get; set; }
    public string ImportantTips { get; set; } = string.Empty;
    public List<TourPlans> TourPlans { get; set; } = new();
    public List<TourTransPorts> TransPorts { get; set; } = new();
    public List<long> Hotels { get; set; } = new();
    public TourPrimaryTransport PrimaryTransport { get; set; } = new();
    public double Price { get; set; }
    public long PictureId { get; set; }
    public int Score { get; set; }
    public Continent TourContinent { get; set; }
    public TourType TourType { get; set; }
    public TourScope TourScope { get; set; }
    public int DayCount { get; set; }
    public bool RequiresVisa { get; set; }
    public List<DateOnly> ExecDate { get; set; } = new();
    public bool IsDeleted { get; set; } = false;

    //seo
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}