using Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Exchange;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommandReq, DeleteCurrencyCommandRes>
{
    private readonly ICurrencyService _currencyService;

    public DeleteCurrencyCommandHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task<DeleteCurrencyCommandRes> Handle(DeleteCurrencyCommandReq request, CancellationToken cancellationToken)
    {

        var currency = await _currencyService.FindByIdAsync(cancellationToken, request.Id);
        if (currency == null)
            return new DeleteCurrencyCommandRes { Success = false, Message = "Currency not found" };

        await _currencyService.BulkRemove(p => p.Id == request.Id, cancellationToken);
        return new DeleteCurrencyCommandRes { Success = true };
    }
}