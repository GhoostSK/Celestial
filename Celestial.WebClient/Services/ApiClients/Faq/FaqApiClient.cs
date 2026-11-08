using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients.Faq;

public class FaqApiClient(HttpJsonClient httpClient)
{
    public async Task<GetFaqListResDto> GetFaqListAsync(GetFaqListReqDto request)
    {
        return await httpClient.GetAsync<GetFaqListResDto>($"/api/{request.Language}/Faq/GetFaqList");
    }
}
