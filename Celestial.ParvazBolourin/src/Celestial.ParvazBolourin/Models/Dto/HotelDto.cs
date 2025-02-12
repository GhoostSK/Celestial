using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Hotel;

namespace Celestial.ParvazBolourin.Api.Models.Dto;

public sealed class HotelDto
{
    public string Name { get; set; } = string.Empty;
    public HotelLocation HotelLocation { get; set; }
    public Continent HotelContinent { get; set; }
    public string Price { get; set; } = string.Empty;
    public string HotelCountry { get; set; } = string.Empty;
    public int HotelScore { get; set; }
    public string BookingUrl { get; set; } = string.Empty;
    public string HotelCode { get; set; } = string.Empty;
    public string HotelCoordinate { get; set; } = string.Empty;
    public string HotelReception { get; set; } = string.Empty;

    [Required] public List<AddPictureReq> Picture { get; set; }

    [Required] public List<HotelServiceLocalizedProperty> LanguageData { get; set; }
}

public sealed class HotelByIdReqDto
{
    [Required] public int LanguageId { get; set; }

    [Required] public int HotelId { get; set; }
}

public sealed class GetHotelListReqDto
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<HotelLocation> HotelLocation { get; set; } = new();
    public List<Continent> HotelContinent { get; set; } = new();
    public long FromPrice { get; set; } = 0;
    public long ToPrice { get; set; } = 0;
    public List<string> HotelCountry { get; set; } = new();
    public List<int> HotelScore { get; set; } = new();
}

public sealed class UpdateHotelReqDto
{
    [Required] public int Id { get; set; }

    [Required] public Hotels Hotel { get; set; }

    [Required] public List<HotelServiceLocalizedProperty> LanguageData { get; set; }
}