using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Api.Identity.Services.UserIdModelBinder;
using Celestial.Agency.Core.Attributes;
using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Hotel.Model;
using Celestial.Agency.Core.Enum.Global;
using Celestial.Agency.Core.Enum.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Models.Dto;

public sealed class HotelDto
{
    [Required] public string Name { get; set; } = string.Empty;

    [Required] public HotelLocation HotelLocation { get; set; }

    public Continent HotelContinent { get; set; }

    [Required] public double Price { get; set; }

    public int CountryId { get; set; }

    public long CityId { get; set; }

    public int StateId { get; set; }

    public int HotelScore { get; set; }

    public string BookingUrl { get; set; } = string.Empty;

    [Required] public string HotelCode { get; set; } = string.Empty;

    public string HotelCoordinate { get; set; } = string.Empty;

    public string HotelReception { get; set; } = string.Empty;

    public List<PictureItem> Picture { get; set; }
    public List<HotelServiceLocalizedProperty> LocalizedProperty { get; set; }

    [FromTokenUserId] public Guid CreatedBy { get; set; }
}

public sealed class GetHotelReqDto
{
    [Slug] [FromRoute] public string Slug { get; set; }
}

public sealed class GetHotelListReqDto
{
    [Required] [FromRoute] public string Language { get; set; }

    public string Title { get; set; } = string.Empty;
    public List<HotelLocation> HotelLocation { get; set; } = new();
    public List<Continent> HotelContinent { get; set; } = new();
    public double FromPrice { get; set; } = 0;
    public double ToPrice { get; set; } = 0;
    public List<string> HotelCountry { get; set; } = new();
    public List<int> HotelScore { get; set; } = new();
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public sealed class UpdateHotelReqDto
{
    [Required] public int Id { get; set; }

    [Required] public Hotel Hotel { get; set; }

    [Required] public List<HotelServiceLocalizedProperty> LocalizedProperty { get; set; }

    [FromTokenUserId] public Guid ModifiedBy { get; set; }
}

public sealed class RemoveHotelReqDto
{
    [Required] public int Id { get; set; }

    [FromTokenUserId] public Guid ModifiedBy { get; set; }
}