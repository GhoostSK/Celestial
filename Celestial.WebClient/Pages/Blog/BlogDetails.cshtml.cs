using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Blog;

public class BlogDetailsModel(IBlogService blogService) : PageModel
{
    public GetBlogBySlugResDto Blog { get; set; } = new();
    public GetBlogCategoryListResDto Category { get; set; } = new();

    public GetLatestBlogListResDto RecentBlog { get; set; } = new();

    public GetBlogTagListResDto Tags { get; set; } = new();
    public string Language { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string? slug)
    {
        if (slug == null) return RedirectToPage("BlogSidebar");
        Language = GetCurrentLang.Execute();
        Blog = await blogService.GetBlogBySlugAsync(new GetBlogBySlugReqDto
        {
            Language = Language,
            Slug = slug
        });

        Category = await blogService.GetBlogCategoriesListAsync(new GetBlogCategoryListReqDto
        {
            Count = 10,
            Language = Language
        });

        RecentBlog = await blogService.GetLatestBlogListAsync(new GetLatestBlogListReqDto
        {
            Count = 3,
            Language = Language
        });
        Tags = await blogService.GetBlogTagsListAsync(new GetBlogTagListReqDto
        {
            Count = 9,
            Language = Language
        });
        if (Blog.HasError)
        {
        }

        return Page();
    }
}