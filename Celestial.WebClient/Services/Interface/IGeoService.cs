using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IGeoService
{
    public Task<GetCountryListResDto> GetCountryListAsync(string language);
    public Task<GetCityListResDto> GetCityListAsync(GetCityListReqDto request);
}
