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
                ErrorMessage = $"Request failed {response.StatusCode}",
                StatusCode = (int)response.StatusCode
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

    public async Task<GetBlogListResDto> GetBlogListAsync(GetBlogListReqDto request)
    {
        return await GetSafeAsync<GetBlogListResDto>(
            $"/api/{request.Language}/Blog/GetBlogsList?PageNumber={request.PageNumber}&PageSize={request.PageSize}&PopularTagCount={request.PopularTagCount}"
        ) ?? new GetBlogListResDto();
    }

    public async Task<GetLatestBlogListResDto> GetLatestBlogListAsync(GetLatestBlogListReqDto request)
    {
        return await GetSafeAsync<GetLatestBlogListResDto>(
            $"/api/{request.Language}/Blog/GetLatestBlog?count={request.Count}"
        ) ?? new GetLatestBlogListResDto();
    }

    public async Task<GetBlogBySlugResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request)
    {
        return await GetSafeAsync<GetBlogBySlugResDto>(
            $"/api/{request.Language}/Blog/GetBlogBySlug/{request.Slug}"
        ) ?? new GetBlogBySlugResDto();
    }

    public async Task<GetBlogCategoryListResDto> GetBlogCategoriesListAsync(GetBlogCategoryListReqDto request)
    {
        return await GetSafeAsync<GetBlogCategoryListResDto>(
            $"/api/{request.Language}/Category/GetList/?count={request.Count}");
    }

    public async Task<GetBlogTagListResDto> GetBlogTagListAsync(GetBlogTagListReqDto request)
    {
        return await GetSafeAsync<GetBlogTagListResDto>(
            $"/api/{request.Language}/BlogTag/GetList/?count={request.Count}");
    }
}