using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients.Currency;

public class CurrencyApiClient(HttpJsonClient httpClient)
{
    public async Task<GetCurrencyListResDto> GetCurrencyListAsync(string language)
    {
        return await httpClient.GetAsync<GetCurrencyListResDto>($"/api/{language}/Currency/GetCurrenciesList");
    }
}
