using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Celestial.WebClient.Pages.Visa;

public class VisaListModel(IVisaService visaService) : PageModel
{
    private const int DefaultPageSize = 12;

    public GetVisaListResDto Visas { get; private set; } = new();

    public string CurrentLanguage { get; private set; } = GetCurrentLang.Execute();

    [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;

    public async Task<IActionResult> OnGetAsync()
    {
        CurrentLanguage = GetCurrentLang.Execute();

        Visas = await visaService.GetVisaListAsync(new GetVisaListReqDto
        {
            Language = CurrentLanguage,
            PageNumber = PageNumber < 1 ? 1 : PageNumber,
            PageSize = DefaultPageSize
        });

        return Page();
    }
}
