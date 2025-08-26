using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients;

public class BlogApiClient(HttpClient http)
{
    public async Task<GetBlogListRes> GetBlogListAsync(GetBlogListReqDto request)
    {
        return await http.GetFromJsonAsync<GetBlogListRes>(
                   $"/api/{request.lang}/Blog/GetBlogsList?PageNumber={request.PageNumber}&PageSize={request.PageSize}")
               ?? new GetBlogListRes();
    }
}