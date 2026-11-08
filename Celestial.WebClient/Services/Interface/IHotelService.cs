using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IHotelService
{
    public Task<GetHotelListResDto> GetHotelListAsync(GetHotelListReqDto request);
    public Task<GetHotelBySlugResDto> GetHotelBySlugAsync(GetHotelBySlugReqDto request);
}
