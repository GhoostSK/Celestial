using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Visa;

public class VisaApiClient(HttpJsonClient httpClient)
{
    public async Task<GetVisaListResDto> GetVisaListAsync(GetVisaListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("PageNumber", request.PageNumber)
            .Add("PageSize", request.PageSize);

        return await httpClient.GetAsync<GetVisaListResDto>(
            $"/api/{request.Language}/Visa/GetVisaList{query}");
    }

    public async Task<GetVisaBySlugResDto> GetVisaBySlugAsync(GetVisaBySlugReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("Slug", request.Slug);

        return await httpClient.GetAsync<GetVisaBySlugResDto>(
            $"/api/{request.Language}/Visa/GetVisaById{query}");
    }
}
