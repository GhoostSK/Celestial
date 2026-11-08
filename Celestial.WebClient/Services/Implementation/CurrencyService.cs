using Celestial.WebClient.Services.ApiClients.Currency;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace Celestial.WebClient.Services.Implementation;

public class CurrencyService(CurrencyApiClient client, IMemoryCache cache) : ICurrencyService
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    /// <summary>
    ///     فهرست ارزها در هر صفحه (در هدر) استفاده می‌شود و کم تغییر می‌کند،
    ///     بنابراین برای جلوگیری از فراخوانی تکراری API کش می‌شود.
    /// </summary>
    public async Task<GetCurrencyListResDto> GetCurrencyListAsync(string language)
    {
        var cacheKey = $"currencies:{language}";

        if (cache.TryGetValue(cacheKey, out GetCurrencyListResDto? cached) && cached is not null)
            return cached;

        var result = await client.GetCurrencyListAsync(language);

        if (!result.HasError)
            cache.Set(cacheKey, result, CacheDuration);

        return result;
    }
}
