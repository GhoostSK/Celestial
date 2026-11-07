using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Pages.Tour;

public class TourDetailsModel(
    ITourService tourService,
    IBookingService bookingService,
    ICommentService commentService,
    IStringLocalizer<SharedResource> localizer) : PageModel
{
    public GetTourBySlugResDto Tour { get; private set; } = new();
    public GetCommentsBySlugResDto Comments { get; private set; } = new();
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();
    public string? Slug { get; private set; }

    [BindProperty] public AddTourBookingReqDto BookingInput { get; set; } = new();

    [BindProperty] public AddCommentReqDto CommentInput { get; set; } = new();

    [TempData] public string? BookingSuccessMessage { get; set; }

    [TempData] public string? CommentSuccessMessage { get; set; }

    public string? BookingErrorMessage { get; private set; }

    public string? CommentErrorMessage { get; private set; }

    /// <summary>ثبت‌نام تور تنها زمانی باز است که تور فعال باشد و مهلت ثبت‌نام نگذشته باشد.</summary>
    public bool IsBookingOpen =>
        !Tour.HasError
        && Tour.Item.TourStatus == TourStatus.Active
        && Tour.Item.DaysUntilRegistrationDeadline >= 0;

    public async Task<IActionResult> OnGetAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Tour/TourSidebar");

        await LoadAsync(slug);

        return Page();
    }

    public async Task<IActionResult> OnPostBookAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Tour/TourSidebar");

        await LoadAsync(slug);

        if (!IsBookingOpen)
        {
            BookingErrorMessage = localizer["Booking_Closed"];
            return Page();
        }

        if (!TryValidateOnly(nameof(BookingInput)))
            return Page();

        BookingInput.Language = CurrentLanguage;
        BookingInput.Slug = Tour.Item.Slug;

        var result = await bookingService.AddTourBookingAsync(BookingInput);

        if (result.HasError)
        {
            BookingErrorMessage = result.ErrorMessage;
            return Page();
        }

        BookingSuccessMessage = string.Format(localizer["Booking_Success"].Value, result.BookingCode);

        return RedirectToPage(new { slug });
    }

    public async Task<IActionResult> OnPostAddCommentAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Tour/TourSidebar");

        await LoadAsync(slug);

        if (string.IsNullOrWhiteSpace(CommentInput.CommentText))
        {
            CommentErrorMessage = localizer["Validation_Required"];
            return Page();
        }

        CommentInput.Language = CurrentLanguage;
        CommentInput.KeyGroup = LocalKeyGroupType.Tour;
        CommentInput.Slug = Tour.Item.Slug;

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

        Tour = await tourService.GetTourBySlugAsync(new GetTourBySlugReqDto
        {
            Language = CurrentLanguage,
            Slug = slug
        });

        if (Tour.HasError)
            return;

        Comments = await commentService.GetCommentsBySlugAsync(new GetCommentsBySlugReqDto
        {
            Language = CurrentLanguage,
            Slug = Tour.Item.Slug,
            LocalGroup = LocalKeyGroupType.Tour
        });

        if (BookingInput.PassengerCount < 1)
            BookingInput.PassengerCount = 1;
    }

    /// <summary>
    ///     صفحه دو فرم مستقل دارد (رزرو و نظرات)؛ این متد خطاهای فرم دیگر را حذف می‌کند
    ///     تا پیام‌های نامرتبط به کاربر نمایش داده نشود.
    /// </summary>
    private bool TryValidateOnly(string prefix)
    {
        foreach (var key in ModelState.Keys
                     .Where(k => !k.StartsWith(prefix, StringComparison.Ordinal))
                     .ToList())
            ModelState.Remove(key);

        return ModelState.IsValid;
    }
}
