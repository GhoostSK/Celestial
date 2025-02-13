using MediatR;

namespace Celestial.ParvazBolourin.Service.Command.Exchange;

public class EditCurrencyCommandReq : IRequest<EditCurrencyCommandRes>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
}

public class EditCurrencyCommandRes
{
}