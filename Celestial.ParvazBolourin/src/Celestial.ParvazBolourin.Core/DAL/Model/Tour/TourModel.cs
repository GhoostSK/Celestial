using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Tour;

public class GetTourListReq
{
    public List<string> Countries { get; set; } = new();
    public List<string> Cities { get; set; }=new();
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public long ToPrice { get; set; }
    public long FromPrice { get; set; }
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();
    public bool IsActiveTour { get; set; }
}