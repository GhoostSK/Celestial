using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Hotel;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Hotel;

public class GetHotelsListReq
{
    public string Name { get; set; } = string.Empty;
    public List<HotelLocation> HotelLocation { get; set; } = new();
    public List<Continent> HotelContinent { get; set; } = new();
    public long FromPrice { get; set; } = 0;
    public long ToPrice { get; set; } = 0;
    public List<int> HotelCountry { get; set; } = new();
    public List<int> HotelScore { get; set; } = new();
}