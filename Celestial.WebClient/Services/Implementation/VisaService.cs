using Celestial.WebClient.Services.ApiClients.Visa;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class VisaService(VisaApiClient client) : IVisaService
{
    public async Task<GetVisaListResDto> GetVisaListAsync(GetVisaListReqDto request)
    {
        return await client.GetVisaListAsync(request);
    }

    public async Task<GetVisaBySlugResDto> GetVisaBySlugAsync(GetVisaBySlugReqDto request)
    {
        return await client.GetVisaBySlugAsync(request);
    }
}
