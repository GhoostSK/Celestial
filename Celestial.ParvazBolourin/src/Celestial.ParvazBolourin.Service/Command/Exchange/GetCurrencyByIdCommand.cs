using MediatR;

namespace Celestial.ParvazBolourin.Service.Command.Exchange;

public class GetCurrencyByIdCommandReq : IRequest<GetCurrencyByIdCommandRes>
{
    public int Id { get; set; }
}

public class GetCurrencyByIdCommandRes
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
}

