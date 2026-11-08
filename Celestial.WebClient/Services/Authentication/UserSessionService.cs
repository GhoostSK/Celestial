using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Celestial.WebClient.Services.Authentication;

public sealed class UserSessionService(IHttpContextAccessor httpContextAccessor) : IUserSessionService
{
    /// <summary>نام آیتمی که توکن API زیر آن در کوکی احراز هویت ذخیره می‌شود.</summary>
    public const string AccessTokenName = "access_token";

    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public string? UserName => httpContextAccessor.HttpContext?.User.Identity?.Name;

    public async Task<string?> GetAccessTokenAsync()
    {
        var context = httpContextAccessor.HttpContext;

        if (context is null || !IsAuthenticated)
            return null;

        return await context.GetTokenAsync(AccessTokenName);
    }

    public async Task SignInAsync(string token, bool rememberMe)
    {
        var context = httpContextAccessor.HttpContext;

        if (context is null || string.IsNullOrWhiteSpace(token))
            return;

        var claims = JwtPayloadReader.ReadClaims(token);
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var properties = new AuthenticationProperties
        {
            IsPersistent = rememberMe,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };
        properties.StoreTokens(new[] { new AuthenticationToken { Name = AccessTokenName, Value = token } });

        await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            properties);
    }

    public async Task SignOutAsync()
    {
        var context = httpContextAccessor.HttpContext;

        if (context is null)
            return;

        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
