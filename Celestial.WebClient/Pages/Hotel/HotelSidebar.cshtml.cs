using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Hotel;

public class HotelSidebarModel(IHotelService hotelService) : PageModel
{
    private const int DefaultPageSize = 6;

    public GetHotelListResDto Hotels { get; private set; } = new();

    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public string? SearchString { get; set; }

    [BindProperty(SupportsGet = true)] public List<string> Countries { get; set; } = new();

    [BindProperty(SupportsGet = true)] public List<HotelLocation> Locations { get; set; } = new();

    [BindProperty(SupportsGet = true)] public List<int> Scores { get; set; } = new();

    [BindProperty(SupportsGet = true)] public double FromPrice { get; set; }

    [BindProperty(SupportsGet = true)] public double ToPrice { get; set; }

    /// <summary>گزینه‌های امتیاز ستاره‌ای که در سایدبار نمایش داده می‌شوند.</summary>
    public static IReadOnlyList<int> ScoreOptions { get; } = new[] { 5, 4, 3, 2, 1 };

    public async Task<IActionResult> OnGetAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        Hotels = await hotelService.GetHotelListAsync(new GetHotelListReqDto
        {
            Language = CurrentLanguage,
            Title = SearchString,
            HotelCountry = Countries,
            HotelLocation = Locations,
            HotelScore = Scores,
            FromPrice = FromPrice,
            ToPrice = ToPrice,
            PageNumber = PageNumber < 1 ? 1 : PageNumber,
            PageSize = DefaultPageSize
        });

        return Page();
    }

    /// <summary>
    ///     مقادیر فیلترهای فعلی را برای ساختن لینک صفحه‌بندی برمی‌گرداند.
    /// </summary>
    public Dictionary<string, string> BuildRouteValues(int? pageNumber = null)
    {
        var values = new Dictionary<string, string>
        {
            ["PageNumber"] = (pageNumber ?? PageNumber).ToString()
        };

        if (!string.IsNullOrWhiteSpace(SearchString))
            values["SearchString"] = SearchString;

        if (FromPrice > 0) values["FromPrice"] = FromPrice.ToString();
        if (ToPrice > 0) values["ToPrice"] = ToPrice.ToString();

        for (var i = 0; i < Countries.Count; i++)
            values[$"Countries[{i}]"] = Countries[i];

        for (var i = 0; i < Locations.Count; i++)
            values[$"Locations[{i}]"] = Locations[i].ToString();

        for (var i = 0; i < Scores.Count; i++)
            values[$"Scores[{i}]"] = Scores[i].ToString();

        return values;
    }
}
