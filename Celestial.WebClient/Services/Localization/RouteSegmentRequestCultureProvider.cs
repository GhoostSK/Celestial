using Microsoft.AspNetCore.Localization;

namespace Celestial.WebClient.Services.Localization;

/// <summary>
///     زبان را از نخستین بخش مسیر درخواست (مثل <c>/en/Tour/TourSidebar</c>) تشخیص می‌دهد.
///     <para>
///         ارائه‌دهنده‌ی پیش‌فرض <see cref="RouteDataRequestCultureProvider" /> به Route Values نگاه می‌کند،
///         اما در این پروژه زبان با یک میان‌افزار از مسیر حذف می‌شود و در Route Values قرار نمی‌گیرد؛
///         به همین دلیل بدون این ارائه‌دهنده، نخستین بازدید از هر زبان با زبان پیش‌فرض رندر می‌شد.
///     </para>
/// </summary>
public sealed class RouteSegmentRequestCultureProvider(IReadOnlyCollection<string> supportedCultures)
    : RequestCultureProvider
{
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var firstSegment = httpContext.Request.Path.Value?
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault();

        if (string.IsNullOrWhiteSpace(firstSegment) ||
            !supportedCultures.Contains(firstSegment, StringComparer.OrdinalIgnoreCase))
            return NullProviderCultureResult;

        return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(firstSegment.ToLowerInvariant()));
    }
}
