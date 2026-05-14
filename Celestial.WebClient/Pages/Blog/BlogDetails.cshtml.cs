using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Blog;

public class BlogDetailsModel(
    IBlogService blogService,
    ICommentService commentClient
) : PageModel
{
    public GetBlogBySlugResDto Blog { get; set; } = new();
    public GetBlogCategoryListResDto Category { get; set; } = new();
    public GetLatestBlogListResDto RecentBlog { get; set; } = new();
    public GetBlogTagListResDto Tags { get; set; } = new();
    public string? Slug { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;

    [BindProperty] public AddCommentReqDto Input { get; set; } = new();

    [TempData] public string? CommentMessage { get; set; }

    [BindProperty]
    public AddCommentReqDto CommentInput { get; set; } = new()
    {
        Language = GetCurrentLang.Execute(),
        KeyGroup = LocalKeyGroupType.Blog,
        Slug = string.Empty,
        ParentCommentId = null
    };

    public async Task<IActionResult> OnGetAsync(string? slug)
    {
        if (slug == null) return RedirectToPage("BlogSidebar");
        Slug = slug;

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

        if (!Blog.HasError && Blog.Item is not null)
        {
            CommentInput.Language = Language;
            CommentInput.KeyGroup = LocalKeyGroupType.Blog;
            CommentInput.Slug = Blog.Item.Slug;
        }

        if (Blog.HasError)
        {
        }

        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAddCommentAsync(string? slug, long? parentId = null)
    {
        CommentInput.Language = string.IsNullOrWhiteSpace(CommentInput.Language)
            ? GetCurrentLang.Execute()
            : CommentInput.Language;

        CommentInput.KeyGroup = LocalKeyGroupType.Blog;


        if (CommentInput.Slug == string.Empty && !string.IsNullOrWhiteSpace(slug))
        {
            var blog = await blogService.GetBlogBySlugAsync(new GetBlogBySlugReqDto
            {
                Language = GetCurrentLang.Execute(),
                Slug = slug
            });

            if (!blog.HasError && blog.Item is not null)
                CommentInput.Slug = blog.Item.Slug;
        }

        if (parentId.HasValue && parentId.Value > 0)
            CommentInput.ParentCommentId = parentId;

        if (string.IsNullOrWhiteSpace(CommentInput.CommentText))
            ModelState.AddModelError(nameof(CommentInput.CommentText), "متن پیام را وارد کنید.");

        if (!ModelState.IsValid)
        {
            CommentMessage = "لطفاً خطاهای فرم را برطرف کنید.";
            await OnGetAsync(slug);
            return Page();
        }

        var result = await commentClient.AddCommentAsync(CommentInput);

        if (result.HasError)
        {
            CommentMessage = result.ErrorMessage ?? "ارسال نظر ناموفق بود.";
            await OnGetAsync(slug);
            return Page();
        }

        CommentMessage = "نظر شما با موفقیت ثبت شد.";

        CommentInput.CommentText = string.Empty;

        return RedirectToPage(new { slug });
    }
}