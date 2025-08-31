using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients;

public class BlogApiClient(HttpClient http)
{
    private async Task<T> GetSafeAsync<T>(string url) where T : ServiceResContextBase, new()
    {
        var response = await http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return new T
            {
                HasError = true,
                ErrorMessage = $"Request failed with status code {response.StatusCode}"
            };

        var result = await response.Content.ReadFromJsonAsync<T>();
        if (result is null)
            return new T
            {
                HasError = true,
                ErrorMessage = "Empty response from server"
            };

        return result;
    }

    public async Task<GetBlogListRes> GetBlogListAsync(GetBlogListReqDto request)
    {
        return await GetSafeAsync<GetBlogListRes>(
            $"/api/{request.Language}/Blog/GetBlogsList?PageNumber={request.PageNumber}&PageSize={request.PageSize}&PopularTagCount={request.PopularTagCount}"
        ) ?? new GetBlogListRes();
    }

    public async Task<List<LatestBlogItemDto>> GetLatestBlogListAsync(GetLatestBlogListReqDto request)
    {
        return await GetSafeAsync<List<LatestBlogItemDto>>(
            $"/api/{request.Language}/Blog/GetLatestBlog?count={request.Count}"
        ) ?? new List<LatestBlogItemDto>();
    }

    public async Task<GetBlogByIdCommandResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request)
    {
        return await GetSafeAsync<GetBlogByIdCommandResDto>(
            $"/api/{request.Language}/Blog/GetBlogBySlug/{request.Slug}"
        ) ?? new GetBlogByIdCommandResDto();
    }
}