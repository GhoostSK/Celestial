using Celestial.WebClient.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Account;

public class LogoutModel(IUserSessionService userSession) : PageModel
{
    public IActionResult OnGet()
    {
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await userSession.SignOutAsync();

        return RedirectToPage("/Index");
    }
}
