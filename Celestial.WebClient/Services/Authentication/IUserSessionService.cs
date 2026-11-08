namespace Celestial.WebClient.Services.Authentication;

/// <summary>
///     نشست کاربر در سمت وب‌کلاینت: توکن دریافتی از API را در کوکی احراز هویت نگه می‌دارد
///     و برای فراخوانی‌های بعدی API در اختیار <c>HttpJsonClient</c> می‌گذارد.
/// </summary>
public interface IUserSessionService
{
    public bool IsAuthenticated { get; }

    public string? UserName { get; }

    public Task<string?> GetAccessTokenAsync();

    public Task SignInAsync(string token, bool rememberMe);

    public Task SignOutAsync();
}
