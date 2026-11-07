using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Tour;

public class TourSidebarModel(ITourService tourService) : PageModel
{
    private const int DefaultPageSize = 6;

    public GetTourListResDto Tours { get; private set; } = new();

    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public string? SearchString { get; set; }

    [BindProperty(SupportsGet = true)] public List<string> Countries { get; set; } = new();

    [BindProperty(SupportsGet = true)] public List<TourType> TourTypes { get; set; } = new();

    [BindProperty(SupportsGet = true)] public List<TourScope> TourScopes { get; set; } = new();

    [BindProperty(SupportsGet = true)] public List<Continent> Continents { get; set; } = new();

    [BindProperty(SupportsGet = true)] public double FromPrice { get; set; }

    [BindProperty(SupportsGet = true)] public double ToPrice { get; set; }

    [BindProperty(SupportsGet = true)] public int FromDay { get; set; }

    [BindProperty(SupportsGet = true)] public int ToDay { get; set; }

    [BindProperty(SupportsGet = true)] public bool ActiveOnly { get; set; }

    [BindProperty(SupportsGet = true)] public TourSortBy SortBy { get; set; } = TourSortBy.Newest;

    /// <summary>گزینه‌های مرتب‌سازی که در کمبوی بالای لیست نمایش داده می‌شوند.</summary>
    public static IReadOnlyDictionary<TourSortBy, string> SortOptions { get; } =
        new Dictionary<TourSortBy, string>
        {
            [TourSortBy.Newest] = "Common_Sort_Newest",
            [TourSortBy.StartDate] = "Common_Sort_Date",
            [TourSortBy.PriceAsc] = "Common_Sort_PriceAsc",
            [TourSortBy.PriceDesc] = "Common_Sort_PriceDesc",
            [TourSortBy.ScoreDesc] = "Common_Sort_Score"
        };

    public async Task<IActionResult> OnGetAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        Tours = await tourService.GetTourListAsync(new GetTourListReqDto
        {
            Language = CurrentLanguage,
            Title = SearchString,
            Countries = Countries,
            TourType = TourTypes,
            TourScope = TourScopes,
            TourContinent = Continents,
            FromPrice = FromPrice,
            ToPrice = ToPrice,
            FromDay = FromDay,
            ToDay = ToDay,
            IsActiveTour = ActiveOnly,
            SortBy = SortBy,
            PageNumber = PageNumber < 1 ? 1 : PageNumber,
            PageSize = DefaultPageSize
        });

        return Page();
    }

    /// <summary>
    ///     مقادیر فیلترهای فعلی را برای ساختن لینک صفحه‌بندی و مرتب‌سازی برمی‌گرداند.
    /// </summary>
    public Dictionary<string, string> BuildRouteValues(int? pageNumber = null, TourSortBy? sortBy = null)
    {
        var values = new Dictionary<string, string>
        {
            ["PageNumber"] = (pageNumber ?? PageNumber).ToString(),
            ["SortBy"] = (sortBy ?? SortBy).ToString()
        };

        if (!string.IsNullOrWhiteSpace(SearchString))
            values["SearchString"] = SearchString;

        if (FromPrice > 0) values["FromPrice"] = FromPrice.ToString();
        if (ToPrice > 0) values["ToPrice"] = ToPrice.ToString();
        if (FromDay > 0) values["FromDay"] = FromDay.ToString();
        if (ToDay > 0) values["ToDay"] = ToDay.ToString();
        if (ActiveOnly) values["ActiveOnly"] = "true";

        for (var i = 0; i < Countries.Count; i++)
            values[$"Countries[{i}]"] = Countries[i];

        for (var i = 0; i < TourTypes.Count; i++)
            values[$"TourTypes[{i}]"] = TourTypes[i].ToString();

        for (var i = 0; i < TourScopes.Count; i++)
            values[$"TourScopes[{i}]"] = TourScopes[i].ToString();

        for (var i = 0; i < Continents.Count; i++)
            values[$"Continents[{i}]"] = Continents[i].ToString();

        return values;
    }
}
