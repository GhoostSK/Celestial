using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Tour;

public sealed class Tours : DbBaseEntity<int>
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
    public bool IsDeleted { get; set; } = false;
}