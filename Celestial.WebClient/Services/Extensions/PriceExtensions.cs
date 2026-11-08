using System.Globalization;

namespace Celestial.WebClient.Services.Extensions;

public static class PriceExtensions
{
    /// <summary>
    ///     قالب‌بندی مبلغ با جداکننده هزارگان بر اساس فرهنگ جاری و افزودن واحد پول (در صورت وجود).
    /// </summary>
    public static string ToPriceString(this decimal price, string? currency = null)
    {
        var amount = price.ToString("N0", CultureInfo.CurrentUICulture);

        return string.IsNullOrWhiteSpace(currency) ? amount : $"{amount} {currency}";
    }

    public static string ToPriceString(this double price, string? currency = null)
    {
        return ((decimal)price).ToPriceString(currency);
    }
}
