using Celestial.WebClient.Services.ApiClients.Geo;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class GeoService(GeoApiClient client) : IGeoService
{
    public async Task<GetCountryListResDto> GetCountryListAsync(string language)
    {
        return await client.GetCountryListAsync(language);
    }

    public async Task<GetCityListResDto> GetCityListAsync(GetCityListReqDto request)
    {
        return await client.GetCityListAsync(request);
    }
}
