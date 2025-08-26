using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Blog;

public class BlogSidebarModel(IBlogService blogService) : PageModel
{
    public GetBlogListRes Blogs { get; set; } = new();


    public async Task OnGetAsync()
    {
        Blogs = await blogService.GetBlogListAsync(new GetBlogListReqDto
        {
            PageNumber = 1,
            PageSize = 10,
            lang = "fa"
        });
    }
}