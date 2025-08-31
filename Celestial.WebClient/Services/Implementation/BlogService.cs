using Celestial.WebClient.Services.ApiClients;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class BlogService(BlogApiClient client) : IBlogService
{
    public async Task<GetBlogListRes> GetBlogListAsync(GetBlogListReqDto request)
    {
        return await client.GetBlogListAsync(request);
    }

    public async Task<List<LatestBlogItemDto>> GetLatestBlogListAsync(GetLatestBlogListReqDto request)
    {
        return await client.GetLatestBlogListAsync(request);
    }

    public async Task<GetBlogByIdCommandResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request)
    {
        return await client.GetBlogBySlugAsync(request);
    }
}