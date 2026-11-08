using Celestial.WebClient.Services.Extensions;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.WebClient.Controller;

/// <summary>
///     واحد پول انتخابی کاربر را در کوکی نگه می‌دارد. فهرست ارزهای مجاز از API خوانده
///     می‌شود تا مقدار دلخواه و نامعتبر در کوکی ذخیره نشود.
/// </summary>
public sealed class CurrencyController(ICurrencyService currencyService) : Microsoft.AspNetCore.Mvc.Controller
{
    public const string CurrencyCookieName = "Celestial.Currency";

    [HttpGet]
    public async Task<IActionResult> SetCurrency(string name, string returnUrl = "/")
    {
        var language = GetCurrentLang.Execute();
        var currencies = await currencyService.GetCurrencyListAsync(language);

        if (currencies.Items.Any(c => c.Name == name))
            Response.Cookies.Append(
                CurrencyCookieName,
                name,
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), HttpOnly = true });

        return Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl) : LocalRedirect($"/{language}");
    }
}
