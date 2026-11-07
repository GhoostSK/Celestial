using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Pages.Hotel;

public class HotelDetailsModel(
    IHotelService hotelService,
    ICommentService commentService,
    IStringLocalizer<SharedResource> localizer) : PageModel
{
    public GetHotelBySlugResDto Hotel { get; private set; } = new();
    public GetCommentsBySlugResDto Comments { get; private set; } = new();
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();
    public string? Slug { get; private set; }

    [BindProperty] public AddCommentReqDto CommentInput { get; set; } = new();

    [TempData] public string? CommentSuccessMessage { get; set; }

    public string? CommentErrorMessage { get; private set; }

    public async Task<IActionResult> OnGetAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Hotel/HotelSidebar");

        await LoadAsync(slug);

        return Page();
    }

    public async Task<IActionResult> OnPostAddCommentAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Hotel/HotelSidebar");

        await LoadAsync(slug);

        if (string.IsNullOrWhiteSpace(CommentInput.CommentText))
        {
            CommentErrorMessage = localizer["Validation_Required"];
            return Page();
        }

        CommentInput.Language = CurrentLanguage;
        CommentInput.KeyGroup = LocalKeyGroupType.Hotel;
        CommentInput.Slug = Hotel.Slug;

        var result = await commentService.AddCommentAsync(CommentInput);

        if (result.HasError)
        {
            CommentErrorMessage = result.ErrorMessage;
            return Page();
        }

        CommentSuccessMessage = localizer["Comment_Success"];

        return RedirectToPage(new { slug });
    }

    private async Task LoadAsync(string slug)
    {
        Slug = slug;
        CurrentLanguage = GetCurrentLang.Execute();

        Hotel = await hotelService.GetHotelBySlugAsync(new GetHotelBySlugReqDto
        {
            Language = CurrentLanguage,
            Slug = slug
        });

        if (Hotel.HasError)
            return;

        Comments = await commentService.GetCommentsBySlugAsync(new GetCommentsBySlugReqDto
        {
            Language = CurrentLanguage,
            Slug = Hotel.Slug,
            LocalGroup = LocalKeyGroupType.Hotel
        });
    }
}
