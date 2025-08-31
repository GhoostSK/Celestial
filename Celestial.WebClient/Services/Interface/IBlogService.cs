using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IBlogService
{
    public Task<GetBlogListRes> GetBlogListAsync(GetBlogListReqDto request);
    public Task<List<LatestBlogItemDto>> GetLatestBlogListAsync(GetLatestBlogListReqDto request);
    public Task<GetBlogByIdCommandResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request);
}