using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Hotel;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Hotel;

public sealed class Hotels : DbBaseEntity<long>
{
    public string Name { get; set; } = string.Empty;
    public HotelLocation HotelLocation { get; set; }
    public Continent HotelContinent { get; set; }
    public double Price { get; set; }
    public int CountryId { get; set; }
    public long CityId { get; set; }
    public int StateId { get; set; }
    public int HotelScore { get; set; }
    public string BookingUrl { get; set; } = string.Empty;
    public string HotelCode { get; set; } = string.Empty;
    public string HotelCoordinate { get; set; } = string.Empty;
    public string HotelReception { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

}