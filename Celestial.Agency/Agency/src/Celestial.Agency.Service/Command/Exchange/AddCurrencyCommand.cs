using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Exchange;

public class AddCurrencyCommandReq : IRequest<AddCurrencyCommandRes>
{
    public string Name { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }  
}

public class AddCurrencyCommandRes : ServiceResContextBase
{
}