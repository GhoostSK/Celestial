using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Hotel;

public class HotelApiClient(HttpJsonClient httpClient)
{
    public async Task<GetHotelListResDto> GetHotelListAsync(GetHotelListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("Title", request.Title)
            .AddRange("HotelLocation", request.HotelLocation)
            .AddRange("HotelContinent", request.HotelContinent)
            .AddRange("HotelCountry", request.HotelCountry)
            .AddRange("HotelScore", request.HotelScore)
            .Add("FromPrice", request.FromPrice)
            .Add("ToPrice", request.ToPrice)
            .Add("PageNumber", request.PageNumber)
            .Add("PageSize", request.PageSize);

        return await httpClient.GetAsync<GetHotelListResDto>(
            $"/api/{request.Language}/Hotel/GetHotelsList{query}");
    }

    public async Task<GetHotelBySlugResDto> GetHotelBySlugAsync(GetHotelBySlugReqDto request)
    {
        return await httpClient.GetAsync<GetHotelBySlugResDto>(
            $"/api/{request.Language}/Hotel/GetHotel/{Uri.EscapeDataString(request.Slug)}");
    }
}
