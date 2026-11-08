using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface ITourService
{
    public Task<GetTourListResDto> GetTourListAsync(GetTourListReqDto request);
    public Task<GetTourBySlugResDto> GetTourBySlugAsync(GetTourBySlugReqDto request);
    public Task<GetFeaturedTourListResDto> GetFeaturedTourListAsync(GetFeaturedTourListReqDto request);
    public Task<GetTourEquipmentListResDto> GetEquipmentListAsync(GetTourEquipmentListReqDto request);
}
