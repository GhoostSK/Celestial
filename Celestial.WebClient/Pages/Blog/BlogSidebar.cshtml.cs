using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Blog;

[IgnoreAntiforgeryToken]
public class BlogSidebarModel(IBlogService blogService, IConfiguration config) : PageModel
{
    public GetBlogListResDto Blogs { get; set; } = new();
    public GetLatestBlogListResDto LatestBlogList { get; set; } = new();
    public string? BaseUrl { get; set; } = string.Empty;
    public string CurrentLanguage { get; set; } = GetCurrentLang.Execute();
    public string SearchString { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int pageNumber = 1, string? searchString = null)
    {
        BaseUrl = config["ApiBaseUrl"] + "/api/en/Global/ReadFile?filePath=";
        Blogs = await blogService.GetBlogListAsync(new GetBlogListReqDto
        {
            PageNumber = pageNumber,
            PageSize = 6,
            Language = CurrentLanguage,
            PopularTagCount = 9,
            Title = searchString
        });
        if (Blogs.HasError)
        {
        }

        LatestBlogList = await blogService.GetLatestBlogListAsync(new GetLatestBlogListReqDto
        {
            Count = 3,
            Language = GetCurrentLang.Execute()
        });
        if (LatestBlogList.HasError)
        {
        }

        return Page();
    }
}