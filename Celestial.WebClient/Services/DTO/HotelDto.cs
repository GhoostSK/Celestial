// ---------------------- Requests ----------------------

using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetHotelListReqDto
{
    public string Language { get; set; } = string.Empty;
    public string? Title { get; set; }
    public List<HotelLocation> HotelLocation { get; set; } = new();
    public List<Continent> HotelContinent { get; set; } = new();
    public List<string> HotelCountry { get; set; } = new();
    public List<int> HotelScore { get; set; } = new();
    public double FromPrice { get; set; }
    public double ToPrice { get; set; }
    public int PageSize { get; set; } = 9;
    public int PageNumber { get; set; } = 1;
}

public sealed class GetHotelBySlugReqDto
{
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

// ---------------------- Responses ----------------------

public sealed class GetHotelListResDto : ServiceResContextBase
{
    public List<HotelListItemDto> Items { get; set; } = new();
    public List<string> CountryList { get; set; } = new();
    public double MinPrice { get; set; }
    public double MaxPrice { get; set; }
    public PaginationMeta Pagination { get; set; } = new();
}

public sealed class GetHotelBySlugResDto : ServiceResContextBase
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string HotelCountry { get; set; } = string.Empty;
    public int HotelScore { get; set; }
    public string BookingUrl { get; set; } = string.Empty;
    public string HotelCode { get; set; } = string.Empty;
    public string MapCoordinate { get; set; } = string.Empty;
    public string HotelReception { get; set; } = string.Empty;
    public string ShortText { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<HotelPictureDto> HotelPictures { get; set; } = new();
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

// ---------------------- DTOs ----------------------

public sealed class HotelListItemDto
{
    public long HotelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HotelCountry { get; set; } = string.Empty;
    public int HotelScore { get; set; }
    public string BookingUrl { get; set; } = string.Empty;
    public string HotelReception { get; set; } = string.Empty;
    public string? ShortText { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public sealed class HotelPictureDto
{
    public long Id { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
