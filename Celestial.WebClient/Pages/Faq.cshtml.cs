using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages;

public class FaqModel(IFaqService faqService) : PageModel
{
    public GetFaqListResDto Faqs { get; private set; } = new();

    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    public async Task<IActionResult> OnGetAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        Faqs = await faqService.GetFaqListAsync(new GetFaqListReqDto
        {
            Language = CurrentLanguage
        });

        return Page();
    }
}
