using Microsoft.AspNetCore.Mvc;

namespace Celestial.WebClient.Controller;

public class LanguageController : Microsoft.AspNetCore.Mvc.Controller
{
    private static readonly string[] SupportedCultures = { "fa", "en", "ar", "ru" };

    /// <summary>
    ///     زبان مقصد از مسیر خوانده می‌شود (مثل <c>/en/Language/SetLanguage</c>)؛ لینک‌های
    ///     تغییر زبان در قالب همین قالب را تولید می‌کنند.
    /// </summary>
    [HttpGet]
    public IActionResult SetLanguage([FromRoute] string culture, string returnUrl = "/")
    {
        if (string.IsNullOrWhiteSpace(culture) || !SupportedCultures.Contains(culture))
            culture = "fa"; // پیش‌فرض

        // فقط جایگزینی culture در URL
        var path = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();

        // اگر culture قبلاً در مسیر بود، جایگزینش کن
        if (segments.Count > 0 && SupportedCultures.Contains(segments[0]))
            segments[0] = culture;
        else
            segments.Insert(0, culture);

        var newPath = "/" + string.Join("/", segments);

        return LocalRedirect(newPath);
    }
}