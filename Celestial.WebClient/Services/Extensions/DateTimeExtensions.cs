using System.Globalization;

namespace Celestial.WebClient.Services.Extensions;

public static class DateTimeExtensions
{
    private static readonly PersianCalendar pc = new();

    private static readonly string[] monthNames =
    {
        "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
        "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
    };

    public static string ToShamsiFullString(this DateTime dateTime)
    {
        try
        {
            var year = pc.GetYear(dateTime);
            var month = pc.GetMonth(dateTime);
            var day = pc.GetDayOfMonth(dateTime);

            return $"{day} {monthNames[month - 1]} {year}";
        }
        catch
        {
            return "Undefined";
        }
    }

    /// <summary>
    ///     تاریخ را بر اساس زبان جاری قالب‌بندی می‌کند: برای فارسی تقویم شمسی و
    ///     برای سایر زبان‌ها تقویم میلادی با قالب فرهنگ جاری.
    /// </summary>
    public static string ToLocalizedDateString(this DateTime dateTime)
    {
        return IsPersianCulture()
            ? dateTime.ToShamsiFullString()
            : dateTime.ToString("d MMMM yyyy", CultureInfo.CurrentUICulture);
    }

    public static string ToLocalizedDateString(this DateOnly date)
    {
        return date.ToDateTime(TimeOnly.MinValue).ToLocalizedDateString();
    }

    public static string ToLocalizedDateString(this DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.LocalDateTime.ToLocalizedDateString();
    }

    private static bool IsPersianCulture()
    {
        return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
            .Equals("fa", StringComparison.OrdinalIgnoreCase);
    }
}
