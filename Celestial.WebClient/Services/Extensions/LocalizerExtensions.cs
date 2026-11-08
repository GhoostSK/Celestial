using Microsoft.Extensions.Localization;

namespace Celestial.WebClient.Services.Extensions;

public static class LocalizerExtensions
{
    /// <summary>
    ///     ترجمه‌ی یک مقدار enum بر اساس قرارداد کلید <c>{EnumName}_{MemberName}</c>
    ///     (مثلاً <c>TourStatus_Active</c>). در صورت نبود کلید، نام عضو برگردانده می‌شود.
    /// </summary>
    public static string Enum<TEnum>(this IStringLocalizer localizer, TEnum value) where TEnum : struct, System.Enum
    {
        var key = $"{typeof(TEnum).Name}_{value}";
        var translation = localizer[key];

        return translation.ResourceNotFound ? value.ToString()! : translation.Value;
    }

    /// <summary>
    ///     انتخاب شکل مفرد یا جمع یک واحد (روز/شب/ستاره) بر اساس تعداد.
    /// </summary>
    public static string Plural(this IStringLocalizer localizer, int count, string singularKey, string pluralKey)
    {
        return count == 1 ? localizer[singularKey] : localizer[pluralKey];
    }
}
