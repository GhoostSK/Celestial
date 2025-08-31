using System.Globalization;

namespace Celestial.WebClient.Services.Extensions;

public class GetCurrentLang
{
    public static string Execute()
    {
        var lang = CultureInfo.CurrentUICulture.Name;

        return lang;
    }
}