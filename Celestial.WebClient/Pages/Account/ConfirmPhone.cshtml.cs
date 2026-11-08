using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Pages.Account;

public class ConfirmPhoneModel(
    IAccountService accountService,
    IStringLocalizer<SharedResource> localizer) : PageModel
{
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty] public ConfirmPhoneReqDto Input { get; set; } = new();

    [TempData] public string? SuccessMessage { get; set; }

    public string? ErrorMessage { get; private set; }

    public string? InfoMessage { get; private set; }

    public async Task<IActionResult> OnGetAsync(string? userName, bool sendCode = false)
    {
        CurrentLanguage = GetCurrentLang.Execute();

        if (string.IsNullOrWhiteSpace(userName))
            return RedirectToPage("/Account/Login");

        Input.UserName = userName;

        if (sendCode)
            await RequestCodeAsync(userName);

        return Page();
    }

    public async Task<IActionResult> OnPostSendCodeAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        if (string.IsNullOrWhiteSpace(Input.UserName))
        {
            ErrorMessage = localizer["Validation_Required"];
            return Page();
        }

        await RequestCodeAsync(Input.UserName);

        return Page();
    }

    public async Task<IActionResult> OnPostConfirmAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        if (!ModelState.IsValid)
            return Page();

        Input.Language = CurrentLanguage;

        var result = await accountService.ConfirmPhoneNumberAsync(Input);

        if (result.HasError)
        {
            ErrorMessage = result.ErrorMessage;
            return Page();
        }

        SuccessMessage = localizer["Account_PhoneConfirmed"];

        return RedirectToPage("/Account/Login");
    }

    private async Task RequestCodeAsync(string userName)
    {
        var result = await accountService.SendConfirmationCodeAsync(new SendConfirmationCodeReqDto
        {
            Language = CurrentLanguage,
            UserName = userName
        });

        if (result.HasError)
            ErrorMessage = result.ErrorMessage;
        else
            InfoMessage = localizer["Account_CodeSent"];
    }
}
