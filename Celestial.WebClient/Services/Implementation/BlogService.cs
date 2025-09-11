using Celestial.WebClient.Services.ApiClients;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class BlogService(BlogApiClient client) : IBlogService
{
    public async Task<GetBlogListResDto> GetBlogListAsync(GetBlogListReqDto request)
    {
        return await client.GetBlogListAsync(request);
    }

    public async Task<GetBlogBySlugResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request)
    {
        return await client.GetBlogBySlugAsync(request);
    }

    public async Task<GetLatestBlogListResDto> GetLatestBlogListAsync(GetLatestBlogListReqDto request)
    {
        return await client.GetLatestBlogListAsync(request);
    }

    public async Task<GetBlogCategoryListResDto> GetBlogCategoriesListAsync(GetBlogCategoryListReqDto request)
    {
        return await client.GetBlogCategoriesListAsync(request);
    }

    public async Task<GetBlogTagListResDto> GetBlogTagsListAsync(GetBlogTagListReqDto request)
    {
        return await client.GetBlogTagListAsync(request);
    }
}