using Celestial.WebClient.Services.ApiClients.Hotel;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class HotelService(HotelApiClient client) : IHotelService
{
    public async Task<GetHotelListResDto> GetHotelListAsync(GetHotelListReqDto request)
    {
        return await client.GetHotelListAsync(request);
    }

    public async Task<GetHotelBySlugResDto> GetHotelBySlugAsync(GetHotelBySlugReqDto request)
    {
        return await client.GetHotelBySlugAsync(request);
    }
}
