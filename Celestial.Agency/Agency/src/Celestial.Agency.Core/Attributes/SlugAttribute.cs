using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Celestial.Agency.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class SlugAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string strValue || string.IsNullOrWhiteSpace(strValue))
            return false;

        var slug = GenerateSlug(strValue);
        return slug == strValue;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} باید یک مقدار معتبر برای Slug باشد.";
    }

    public static string GenerateSlug(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        input = input.ToLowerInvariant();
        input = Regex.Replace(input, @"\s+", "-"); // جایگزینی فاصله‌ها با "-"
        input = Regex.Replace(input, @"[^\w\-]", ""); // حذف کاراکترهای غیرمجاز
        input = Regex.Replace(input, @"-+", "-"); // حذف "-" های تکراری
        return input.Trim('-');
    }
}