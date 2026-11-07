using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Pages;

public class ContactModel(
    IContactUsService contactUsService,
    IStringLocalizer<SharedResource> localizer) : PageModel
{
    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty] public AddContactUsReqDto Input { get; set; } = new();

    [TempData] public string? SuccessMessage { get; set; }

    public string? ErrorMessage { get; private set; }

    public void OnGet()
    {
        CurrentLanguage = GetCurrentLang.Execute();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        if (!ModelState.IsValid)
            return Page();

        Input.Language = CurrentLanguage;

        var result = await contactUsService.AddContactUsAsync(Input);

        if (result.HasError)
        {
            ErrorMessage = string.IsNullOrWhiteSpace(result.ErrorMessage)
                ? localizer["Common_Error"]
                : result.ErrorMessage;

            return Page();
        }

        SuccessMessage = localizer["Contact_Success"];

        return RedirectToPage();
    }
}
