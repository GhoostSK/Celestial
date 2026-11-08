using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Geo;

public class GeoApiClient(HttpJsonClient httpClient)
{
    public async Task<GetCountryListResDto> GetCountryListAsync(string language)
    {
        return await httpClient.GetAsync<GetCountryListResDto>($"/api/{language}/Geo/CountryList");
    }

    public async Task<GetCityListResDto> GetCityListAsync(GetCityListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("countryId", request.CountryId);

        return await httpClient.GetAsync<GetCityListResDto>(
            $"/api/{request.Language}/Geo/CityList{query}");
    }
}
