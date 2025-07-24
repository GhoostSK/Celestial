using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Exchange;

public class DeleteCurrencyCommandReq : IRequest<DeleteCurrencyCommandRes>
{
    public int Id { get; set; }
}

public class DeleteCurrencyCommandRes : ServiceResContextBase
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}