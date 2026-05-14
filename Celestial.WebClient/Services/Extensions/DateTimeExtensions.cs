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
}