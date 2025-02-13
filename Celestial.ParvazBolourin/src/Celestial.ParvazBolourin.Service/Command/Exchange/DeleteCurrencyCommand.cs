using MediatR;

namespace Celestial.ParvazBolourin.Service.Command.Exchange;

public class DeleteCurrencyCommandReq : IRequest<DeleteCurrencyCommandRes>
{
    public int Id { get; set; }
}

public class DeleteCurrencyCommandRes
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}