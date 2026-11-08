using Celestial.WebClient.Services.ApiClients.Tour;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class TourService(TourApiClient client) : ITourService
{
    public async Task<GetTourListResDto> GetTourListAsync(GetTourListReqDto request)
    {
        return await client.GetTourListAsync(request);
    }

    public async Task<GetTourBySlugResDto> GetTourBySlugAsync(GetTourBySlugReqDto request)
    {
        return await client.GetTourBySlugAsync(request);
    }

    public async Task<GetFeaturedTourListResDto> GetFeaturedTourListAsync(GetFeaturedTourListReqDto request)
    {
        return await client.GetFeaturedTourListAsync(request);
    }

    public async Task<GetTourEquipmentListResDto> GetEquipmentListAsync(GetTourEquipmentListReqDto request)
    {
        return await client.GetEquipmentListAsync(request);
    }
}
