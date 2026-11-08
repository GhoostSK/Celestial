using Celestial.WebClient.Services.Authentication;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Pages.Account;

public class LoginModel(
    IAccountService accountService,
    IUserSessionService userSession,
    IStringLocalizer<SharedResource> localizer) : PageModel
{
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty] public LoginReqDto Input { get; set; } = new();

    [BindProperty(SupportsGet = true)] public string? ReturnUrl { get; set; }

    public string? ErrorMessage { get; private set; }

    /// <summary>وقتی شماره تلفن تأیید نشده باشد، کاربر باید به صفحه‌ی تأیید هدایت شود.</summary>
    public bool RequiresPhoneConfirmation { get; private set; }

    public IActionResult OnGet()
    {
        if (userSession.IsAuthenticated)
            return RedirectToPage("/Index");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        if (!ModelState.IsValid)
            return Page();

        Input.Language = CurrentLanguage;

        var result = await accountService.LoginAsync(Input);

        if (result.HasError)
        {
            // API برای شماره‌ی تأییدنشده کد 403 برمی‌گرداند
            RequiresPhoneConfirmation = result.StatusCode == StatusCodes.Status403Forbidden;
            ErrorMessage = string.IsNullOrWhiteSpace(result.ErrorMessage)
                ? localizer["Account_LoginFailed"]
                : result.ErrorMessage;

            return Page();
        }

        if (string.IsNullOrWhiteSpace(result.Token))
        {
            ErrorMessage = localizer["Account_LoginFailed"];
            return Page();
        }

        await userSession.SignInAsync(result.Token, Input.RememberMe);

        return RedirectToLocalOrHome();
    }

    private IActionResult RedirectToLocalOrHome()
    {
        return !string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl)
            ? LocalRedirect(ReturnUrl)
            : RedirectToPage("/Index");
    }
}
