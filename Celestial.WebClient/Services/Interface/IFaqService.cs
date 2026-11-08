using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IFaqService
{
    public Task<GetFaqListResDto> GetFaqListAsync(GetFaqListReqDto request);
}
