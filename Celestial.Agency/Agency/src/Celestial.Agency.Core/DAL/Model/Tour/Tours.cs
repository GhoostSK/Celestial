using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Agency.Core.DAL.Model.Tour.TourEquipment;
using Celestial.Agency.Core.Enum.Global;
using Celestial.Agency.Core.Enum.Tour;
using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Tour;

public sealed class Tours : DbBaseEntity<long>
{
    public List<Country> Countries { get; set; } = new();
    public List<City> Cities { get; set; } = new();
    public string TourCode { get; set; } = string.Empty;
    public TourStatus TourStatus { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly RegistrationDeadline { get; set; }
    public int PassengerCapacity { get; set; }
    public int TourUserRegistrations { get; set; }
    public List<TourPlans> TourPlans { get; set; } = new();
    public List<TourEquipments> UnNecessaryEquipment { get; set; } = new();
    public List<TourEquipments> NecessaryEquipment { get; set; } = new();
    public List<TourTransPorts> TransPorts { get; set; } = new();
    public List<Hotels> Hotels { get; set; } = new();
    public TourPrimaryTransport PrimaryTransport { get; set; } = new();
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    public long PictureId { get; set; }
    public int Score { get; set; }
    public Continent TourContinent { get; set; }
    public TourType TourType { get; set; }
    public TourScope TourScope { get; set; }
    public bool RequiresVisa { get; set; }
    public List<DateOnly> ExecDate { get; set; } = new();
    public int TotalDay { get; set; } = 0;
    public long Views { get; set; }
    public bool IsDeleted { get; set; } = false;
}