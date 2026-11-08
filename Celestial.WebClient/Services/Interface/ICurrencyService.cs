using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface ICurrencyService
{
    public Task<GetCurrencyListResDto> GetCurrencyListAsync(string language);
}
