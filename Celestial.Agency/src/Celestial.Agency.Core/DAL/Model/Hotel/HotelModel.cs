using Celestial.Agency.Core.Enum.Global;
using Celestial.Agency.Core.Enum.Hotel;

namespace Celestial.Agency.Core.DAL.Model.Hotel;

public class GetHotelsListReq
{
    public string Title { get; set; } = string.Empty;
    public List<HotelLocation> HotelLocation { get; set; } = new();
    public List<Continent> HotelContinent { get; set; } = new();
    public double FromPrice { get; set; } = 0;
    public double ToPrice { get; set; } = 0;
    public List<int> HotelCountry { get; set; } = new();
    public List<int> HotelScore { get; set; } = new();
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}