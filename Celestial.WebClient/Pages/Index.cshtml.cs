using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.Agency.web.Pages;

public class IndexModel(
    ITourService tourService,
    IHotelService hotelService,
    IBlogService blogService) : PageModel
{
    private const int SectionItemCount = 6;

    public GetFeaturedTourListResDto FeaturedTours { get; private set; } = new();
    public GetHotelListResDto Hotels { get; private set; } = new();
    public GetLatestBlogListResDto LatestBlogs { get; private set; } = new();

    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    public async Task<IActionResult> OnGetAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        FeaturedTours = await tourService.GetFeaturedTourListAsync(new GetFeaturedTourListReqDto
        {
            Language = CurrentLanguage,
            TopCount = SectionItemCount
        });

        Hotels = await hotelService.GetHotelListAsync(new GetHotelListReqDto
        {
            Language = CurrentLanguage,
            PageNumber = 1,
            PageSize = SectionItemCount
        });

        LatestBlogs = await blogService.GetLatestBlogListAsync(new GetLatestBlogListReqDto
        {
            Language = CurrentLanguage,
            Count = SectionItemCount
        });

        return Page();
    }
}
