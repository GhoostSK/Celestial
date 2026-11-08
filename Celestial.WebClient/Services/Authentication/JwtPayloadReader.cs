using System.Security.Claims;
using System.Text.Json;

namespace Celestial.WebClient.Services.Authentication;

/// <summary>
///     Claim های یک توکن JWT را فقط برای نمایش در رابط کاربری می‌خواند.
///     <para>
///         اعتبارسنجی امضا لازم نیست، چون توکن هرگز از سمت مرورگر نمی‌آید؛ مستقیماً از پاسخ API
///         گرفته و در کوکی احراز هویت رمزنگاری‌شده نگه داشته می‌شود. اعتبار واقعی توکن
///         هنگام فراخوانی API توسط خود API بررسی می‌شود.
///     </para>
/// </summary>
public static class JwtPayloadReader
{
    private const string NameIdentifierClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    private const string NameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    private const string RoleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

    public static List<Claim> ReadClaims(string token)
    {
        var claims = new List<Claim>();
        var parts = token.Split('.');

        if (parts.Length < 2)
            return claims;

        JsonDocument document;

        try
        {
            document = JsonDocument.Parse(DecodeBase64Url(parts[1]));
        }
        catch (Exception exception) when (exception is JsonException or FormatException)
        {
            return claims;
        }

        using (document)
        {
            foreach (var property in document.RootElement.EnumerateObject())
            {
                var claimType = property.Name switch
                {
                    NameIdentifierClaim or "sub" => ClaimTypes.NameIdentifier,
                    NameClaim or "unique_name" => ClaimTypes.Name,
                    RoleClaim or "role" => ClaimTypes.Role,
                    _ => property.Name
                };

                switch (property.Value.ValueKind)
                {
                    case JsonValueKind.Array:
                        claims.AddRange(property.Value.EnumerateArray()
                            .Select(item => item.ToString())
                            .Where(value => !string.IsNullOrWhiteSpace(value))
                            .Select(value => new Claim(claimType, value)));
                        break;

                    case JsonValueKind.Object:
                        break;

                    default:
                        var text = property.Value.ToString();
                        if (!string.IsNullOrWhiteSpace(text))
                            claims.Add(new Claim(claimType, text));
                        break;
                }
            }
        }

        return claims;
    }

    private static byte[] DecodeBase64Url(string value)
    {
        var normalized = value.Replace('-', '+').Replace('_', '/');

        return Convert.FromBase64String(normalized.PadRight(normalized.Length + (4 - normalized.Length % 4) % 4, '='));
    }
}
