using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Hotel;

namespace Celestial.ParvazBolourin.Api.Models.Dto;

public sealed class HotelDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public HotelLocation HotelLocation { get; set; }
    [Required]
    public Continent HotelContinent { get; set; }
    [Required]
    public string Price { get; set; } = string.Empty;
    [Required]
    public int HotelCountry { get; set; }
    [Required]
    public long HotelCity { get; set; }
    [Required]
    public int HotelState { get; set; }
    [Required]
    public int HotelScore { get; set; }
    [Required]
    public string BookingUrl { get; set; } = string.Empty;
    [Required]
    public string HotelCode { get; set; } = string.Empty;
    [Required]
    public string HotelCoordinate { get; set; } = string.Empty;
    [Required]
    public string HotelReception { get; set; } = string.Empty;
    public List<AddPictureReq> Picture { get; set; }

    [Required]
    public List<HotelServiceLocalizedProperty> LanguageData { get; set; }
}

public sealed class HotelByIdReqDto
{
    [Required] public int LanguageId { get; set; }

    [Required] public int HotelId { get; set; }
}

public sealed class GetHotelListReqDto
{
    [Required]
    public int LanguageId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public List<HotelLocation> HotelLocation { get; set; } = new();
    [Required]
    public List<Continent> HotelContinent { get; set; } = new();
    [Required]
    public long FromPrice { get; set; } = 0;
    [Required]
    public long ToPrice { get; set; } = 0;
    [Required]
    public List<string> HotelCountry { get; set; } = new();
    [Required]
    public List<int> HotelScore { get; set; } = new();
}

public sealed class UpdateHotelReqDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Hotels Hotel { get; set; }

    [Required]
    public List<HotelServiceLocalizedProperty> LanguageData { get; set; }
}