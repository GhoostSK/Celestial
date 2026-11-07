using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Pages.Blog;

public class BlogDetailsModel(
    IBlogService blogService,
    ICommentService commentService,
    IStringLocalizer<SharedResource> localizer) : PageModel
{
    public GetBlogBySlugResDto Blog { get; private set; } = new();
    public GetBlogCategoryListResDto Category { get; private set; } = new();
    public GetLatestBlogListResDto RecentBlog { get; private set; } = new();
    public GetBlogTagListResDto Tags { get; private set; } = new();
    public GetCommentsBySlugResDto Comments { get; private set; } = new();

    public string? Slug { get; private set; }
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty] public AddCommentReqDto CommentInput { get; set; } = new();

    [TempData] public string? CommentMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Blog/BlogSidebar");

        await LoadAsync(slug);

        return Page();
    }

    public async Task<IActionResult> OnPostAddCommentAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Blog/BlogSidebar");

        await LoadAsync(slug);

        if (string.IsNullOrWhiteSpace(CommentInput.CommentText))
        {
            ModelState.AddModelError(nameof(CommentInput.CommentText), localizer["Validation_Required"]);
            CommentMessage = localizer["Validation_Required"];

            return Page();
        }

        CommentInput.Language = CurrentLanguage;
        CommentInput.KeyGroup = LocalKeyGroupType.Blog;
        CommentInput.Slug = Blog.Item.Slug;

        var result = await commentService.AddCommentAsync(CommentInput);

        if (result.HasError)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            CommentMessage = result.ErrorMessage;

            return Page();
        }

        CommentMessage = localizer["Comment_Success"];

        return RedirectToPage(new { slug });
    }

    private async Task LoadAsync(string slug)
    {
        Slug = slug;
        CurrentLanguage = GetCurrentLang.Execute();

        Blog = await blogService.GetBlogBySlugAsync(new GetBlogBySlugReqDto
        {
            Language = CurrentLanguage,
            Slug = slug
        });

        Category = await blogService.GetBlogCategoriesListAsync(new GetBlogCategoryListReqDto
        {
            Count = 10,
            Language = CurrentLanguage
        });

        RecentBlog = await blogService.GetLatestBlogListAsync(new GetLatestBlogListReqDto
        {
            Count = 3,
            Language = CurrentLanguage
        });

        Tags = await blogService.GetBlogTagsListAsync(new GetBlogTagListReqDto
        {
            Count = 9,
            Language = CurrentLanguage
        });

        if (Blog.HasError)
            return;

        Comments = await commentService.GetCommentsBySlugAsync(new GetCommentsBySlugReqDto
        {
            Language = CurrentLanguage,
            Slug = Blog.Item.Slug,
            LocalGroup = LocalKeyGroupType.Blog
        });
    }
}
