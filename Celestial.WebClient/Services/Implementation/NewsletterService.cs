using Celestial.WebClient.Services.ApiClients.Newsletter;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class NewsletterService(NewsletterApiClient client) : INewsletterService
{
    public async Task<SubscribeNewsletterResDto> SubscribeAsync(SubscribeNewsletterReqDto request)
    {
        return await client.SubscribeAsync(request);
    }
}
