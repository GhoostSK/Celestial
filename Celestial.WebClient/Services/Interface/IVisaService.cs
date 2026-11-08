using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IVisaService
{
    public Task<GetVisaListResDto> GetVisaListAsync(GetVisaListReqDto request);
    public Task<GetVisaBySlugResDto> GetVisaBySlugAsync(GetVisaBySlugReqDto request);
}
