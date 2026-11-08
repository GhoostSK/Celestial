using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetCurrencyListResDto : ServiceResContextBase
{
    public List<CurrencyItemDto> Items { get; set; } = new();
}

public sealed class CurrencyItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
}
