using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.ApiClients.Blog;

public class BlogApiClient(HttpJsonClient httpClient)
{
    public async Task<GetBlogListResDto> GetBlogListAsync(GetBlogListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("Title", request.Title)
            .AddRange("CategoriesId", request.CategoryIds)
            .AddRange("TagsId", request.TagIds)
            .Add("PopularTagCount", request.PopularTagCount)
            .Add("PageNumber", request.PageNumber)
            .Add("PageSize", request.PageSize);

        return await httpClient.GetAsync<GetBlogListResDto>(
            $"/api/{request.Language}/Blog/GetBlogsList{query}");
    }

    public async Task<GetLatestBlogListResDto> GetLatestBlogListAsync(GetLatestBlogListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("Count", request.Count);

        return await httpClient.GetAsync<GetLatestBlogListResDto>(
            $"/api/{request.Language}/Blog/GetLatestBlog{query}");
    }

    public async Task<GetBlogBySlugResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request)
    {
        return await httpClient.GetAsync<GetBlogBySlugResDto>(
            $"/api/{request.Language}/Blog/GetBlogBySlug/{Uri.EscapeDataString(request.Slug ?? string.Empty)}");
    }

    public async Task<GetBlogCategoryListResDto> GetBlogCategoriesListAsync(GetBlogCategoryListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("count", request.Count);

        return await httpClient.GetAsync<GetBlogCategoryListResDto>(
            $"/api/{request.Language}/Category/GetList{query}");
    }

    public async Task<GetBlogTagListResDto> GetBlogTagListAsync(GetBlogTagListReqDto request)
    {
        var query = new ApiQueryBuilder()
            .Add("count", request.Count);

        return await httpClient.GetAsync<GetBlogTagListResDto>(
            $"/api/{request.Language}/BlogTag/GetList{query}");
    }
}
