using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Exchange;

public class GetCurrencyByIdCommandReq : IRequest<GetCurrencyByIdCommandRes>
{
    public int Id { get; set; }
}

public class GetCurrencyByIdCommandRes : ServiceResContextBase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
}