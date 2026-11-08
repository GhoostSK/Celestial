using Celestial.WebClient.Services.ApiClients.Faq;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class FaqService(FaqApiClient client) : IFaqService
{
    public async Task<GetFaqListResDto> GetFaqListAsync(GetFaqListReqDto request)
    {
        return await client.GetFaqListAsync(request);
    }
}
