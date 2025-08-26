using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IBlogService
{
    Task<GetBlogListRes> GetBlogListAsync(GetBlogListReqDto request);
}