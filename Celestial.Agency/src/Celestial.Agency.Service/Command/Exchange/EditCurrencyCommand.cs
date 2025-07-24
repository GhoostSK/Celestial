using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Exchange;

public class EditCurrencyCommandReq : IRequest<EditCurrencyCommandRes>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
}

public class EditCurrencyCommandRes : ServiceResContextBase
{
}