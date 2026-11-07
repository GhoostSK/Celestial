using Celestial.Agency.Core.Enum.Global;
using Celestial.Agency.Core.Enum.Tour;

namespace Celestial.Agency.Core.DAL.Model.Tour;

public class GetTourListReq
{
    public List<int> Countries { get; set; } = new();
    public List<long> Cities { get; set; } = new();
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public decimal ToPrice { get; set; }
    public decimal FromPrice { get; set; }
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public bool IsActiveTour { get; set; }
    public TourSortBy SortBy { get; set; } = TourSortBy.Newest;
}