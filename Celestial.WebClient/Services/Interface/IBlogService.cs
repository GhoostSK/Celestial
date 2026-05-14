using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IBlogService
{
    public Task<GetBlogListResDto> GetBlogListAsync(GetBlogListReqDto request);
    public Task<GetLatestBlogListResDto> GetLatestBlogListAsync(GetLatestBlogListReqDto request);
    public Task<GetBlogBySlugResDto> GetBlogBySlugAsync(GetBlogBySlugReqDto request);
    public Task<GetBlogCategoryListResDto> GetBlogCategoriesListAsync(GetBlogCategoryListReqDto request);
    public Task<GetBlogTagListResDto> GetBlogTagsListAsync(GetBlogTagListReqDto request);
}