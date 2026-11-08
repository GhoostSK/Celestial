using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetCityListReqDto
{
    public string Language { get; set; } = string.Empty;
    public int CountryId { get; set; }
}

public sealed class GetCountryListResDto : ServiceResContextBase
{
    public List<CountryItemDto> Items { get; set; } = new();
}

public sealed class GetCityListResDto : ServiceResContextBase
{
    public List<CityItemDto> Items { get; set; } = new();
}

public sealed class CountryItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Shortname { get; set; } = string.Empty;
    public int PhoneCode { get; set; }
}

public sealed class CityItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
