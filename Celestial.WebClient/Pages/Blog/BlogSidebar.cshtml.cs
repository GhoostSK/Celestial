using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Blog;

public class BlogSidebarModel(IBlogService blogService) : PageModel
{
    private const int DefaultPageSize = 6;

    public GetBlogListResDto Blogs { get; private set; } = new();
    public GetLatestBlogListResDto LatestBlogList { get; private set; } = new();
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public string? SearchString { get; set; }

    [BindProperty(SupportsGet = true)] public List<long> CategoryIds { get; set; } = new();

    [BindProperty(SupportsGet = true)] public List<long> TagIds { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        Blogs = await blogService.GetBlogListAsync(new GetBlogListReqDto
        {
            PageNumber = PageNumber < 1 ? 1 : PageNumber,
            PageSize = DefaultPageSize,
            Language = CurrentLanguage,
            PopularTagCount = 9,
            Title = SearchString,
            CategoryIds = CategoryIds,
            TagIds = TagIds
        });

        LatestBlogList = await blogService.GetLatestBlogListAsync(new GetLatestBlogListReqDto
        {
            Count = 3,
            Language = CurrentLanguage
        });

        return Page();
    }

    /// <summary>
    ///     مقادیر فیلترهای فعلی را برای ساختن لینک صفحه‌بندی و فیلترها برمی‌گرداند.
    /// </summary>
    public Dictionary<string, string> BuildRouteValues(int? pageNumber = null, long? categoryId = null,
        long? tagId = null)
    {
        var values = new Dictionary<string, string>
        {
            ["PageNumber"] = (pageNumber ?? PageNumber).ToString()
        };

        if (!string.IsNullOrWhiteSpace(SearchString))
            values["SearchString"] = SearchString;

        var categories = categoryId.HasValue ? new List<long> { categoryId.Value } : CategoryIds;
        var tags = tagId.HasValue ? new List<long> { tagId.Value } : TagIds;

        for (var i = 0; i < categories.Count; i++)
            values[$"CategoryIds[{i}]"] = categories[i].ToString();

        for (var i = 0; i < tags.Count; i++)
            values[$"TagIds[{i}]"] = tags[i].ToString();

        return values;
    }
}
