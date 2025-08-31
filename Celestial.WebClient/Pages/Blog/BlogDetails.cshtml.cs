using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Blog;

public class BlogDetailsModel(IBlogService blogService) : PageModel
{
    public GetBlogByIdCommandResDto Blog { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string? slug)
    {
        if (slug == null) return RedirectToPage("BlogSidebar");

        Blog = await blogService.GetBlogBySlugAsync(new GetBlogBySlugReqDto
        {
            Language = GetCurrentLang.Execute(),
            Slug = slug
        });
        return Page();
    }
}