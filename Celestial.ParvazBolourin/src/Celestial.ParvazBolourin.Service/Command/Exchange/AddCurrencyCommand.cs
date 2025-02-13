using MediatR;

namespace Celestial.ParvazBolourin.Service.Command.Exchange;

public class AddCurrencyCommandReq : IRequest<AddCurrencyCommandRes>
{
    public string Name { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
}
public class AddCurrencyCommandRes
{

}