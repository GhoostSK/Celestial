using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients.Newsletter;

public class NewsletterApiClient(HttpJsonClient httpClient)
{
    public async Task<SubscribeNewsletterResDto> SubscribeAsync(SubscribeNewsletterReqDto request)
    {
        return await httpClient.PostAsync<SubscribeNewsletterReqDto, SubscribeNewsletterResDto>(
            $"/api/{request.Language}/Newsletter/Subscribe",
            request);
    }
}
