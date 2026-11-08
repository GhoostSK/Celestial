using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Account;

public class RegisterModel(IAccountService accountService) : PageModel
{
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty] public RegisterReqDto Input { get; set; } = new();

    public string? ErrorMessage { get; private set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        if (!ModelState.IsValid)
            return Page();

        Input.Language = CurrentLanguage;

        var result = await accountService.RegisterAsync(Input);

        if (result.HasError)
        {
            ErrorMessage = result.ErrorMessage;
            return Page();
        }

        // پس از ثبت‌نام باید شماره تلفن تأیید شود
        return RedirectToPage("/Account/ConfirmPhone", new { userName = Input.UserName, sendCode = true });
    }
}
