using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Visa;

public class VisaDetailsModel(IVisaService visaService) : PageModel
{
    public GetVisaBySlugResDto Visa { get; private set; } = new();

    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    public string? Slug { get; private set; }

    public async Task<IActionResult> OnGetAsync(string? slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return RedirectToPage("/Visa/VisaList");

        Slug = slug;
        CurrentLanguage = GetCurrentLang.Execute();

        Visa = await visaService.GetVisaBySlugAsync(new GetVisaBySlugReqDto
        {
            Language = CurrentLanguage,
            Slug = slug
        });

        return Page();
    }
}
