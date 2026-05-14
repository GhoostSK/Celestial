using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients.Blog;

public class BlogApiClient(HttpJsonClient httpClient)
{
    public async Task<GetBlogListResDto> GetBlogListAsync(GetBlogListReqDto request)
    {
        return await httpClient.GetAsync<GetBlogListResDto>(
            $"/api/{request.Language}/Blog/GetBlogsList?PageNumber={request.PageNumber}&PageSize={request.PageSize}&PopularTagCount={request.PopularTagCount}&Title={request.Title}"
        ) ?? new GetBlogListResDto();
    }

    public async Task<GetLatestBlogListResDto> GetLatestBlogListAsync(GetLatestBlogListReqDto request)
    {
        return await httpClient.GetAsync<GetLatestBlogListResDto>(
            $"/api/{request.Language}/Blog/GetLatestBlog?count={request.Count}"
        ) ?? new GetLatestBlogListResDto();
    }

    public async Task<GetBlogBySlugResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request)
    {
        return await httpClient.GetAsync<GetBlogBySlugResDto>(
            $"/api/{request.Language}/Blog/GetBlogBySlug/{request.Slug}"
        ) ?? new GetBlogBySlugResDto();
    }

    public async Task<GetBlogCategoryListResDto> GetBlogCategoriesListAsync(GetBlogCategoryListReqDto request)
    {
        return await httpClient.GetAsync<GetBlogCategoryListResDto>(
            $"/api/{request.Language}/Category/GetList/?count={request.Count}");
    }

    public async Task<GetBlogTagListResDto> GetBlogTagListAsync(GetBlogTagListReqDto request)
    {
        return await httpClient.GetAsync<GetBlogTagListResDto>(
            $"/api/{request.Language}/BlogTag/GetList/?count={request.Count}");
    }
}