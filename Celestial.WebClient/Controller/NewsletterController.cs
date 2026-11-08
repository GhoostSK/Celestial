using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Controller;

/// <summary>
///     فرم خبرنامه در فوتر و صفحه‌ی تماس تکرار می‌شود، بنابراین ارسال آن در یک کنترلر
///     مشترک انجام می‌شود و کاربر به همان صفحه‌ی قبلی برمی‌گردد.
/// </summary>
public sealed class NewsletterController(
    INewsletterService newsletterService,
    IStringLocalizer<SharedResource> localizer) : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subscribe([FromForm] string? email, string? returnUrl)
    {
        var language = GetCurrentLang.Execute();

        if (string.IsNullOrWhiteSpace(email))
        {
            TempData["NewsletterError"] = localizer["Validation_Required"].Value;

            return RedirectBack(returnUrl, language);
        }

        var result = await newsletterService.SubscribeAsync(new SubscribeNewsletterReqDto
        {
            Language = language,
            Email = email
        });

        if (result.HasError)
            TempData["NewsletterError"] = string.IsNullOrWhiteSpace(result.ErrorMessage)
                ? localizer["Common_Error"].Value
                : result.ErrorMessage;
        else
            TempData["NewsletterSuccess"] = result.AlreadySubscribed
                ? localizer["Newsletter_AlreadySubscribed"].Value
                : localizer["Newsletter_Success"].Value;

        return RedirectBack(returnUrl, language);
    }

    private IActionResult RedirectBack(string? returnUrl, string language)
    {
        return !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? LocalRedirect(returnUrl)
            : LocalRedirect($"/{language}");
    }
}
