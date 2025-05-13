using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.Attributes;
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
    public double Price { get; set; }
    [Required]
    public int CountryId { get; set; }
    [Required]
    public long CityId { get; set; }
    [Required]
    public int StateId { get; set; }
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
    public List<PictureItem> Picture { get; set; }
    [Required]
    public List<HotelServiceLocalizedProperty> LanguageData { get; set; }
}





public sealed class HotelByIdReqDto
{
    public int LanguageId { get; set; }
    public int HotelId { get; set; }
    [Slug]
    public string Slug { get; set; }
}

public sealed class GetHotelListReqDto
{
    [Required]
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<HotelLocation> HotelLocation { get; set; } = new();
    public List<Continent> HotelContinent { get; set; } = new();
    public double FromPrice { get; set; } = 0;
    public double ToPrice { get; set; } = 0;
    public List<string> HotelCountry { get; set; } = new();
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