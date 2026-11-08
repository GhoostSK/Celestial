using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface INewsletterService
{
    public Task<SubscribeNewsletterResDto> SubscribeAsync(SubscribeNewsletterReqDto request);
}
