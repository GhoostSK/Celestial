using Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using MediatR;

namespace Celestial.Service.CommandHandler.Exchange;

public class DeleteCurrencyCommandHandler(ICurrencyService currencyService)
    : IRequestHandler<DeleteCurrencyCommandReq, DeleteCurrencyCommandRes>
{
    public async Task<DeleteCurrencyCommandRes> Handle(DeleteCurrencyCommandReq request, CancellationToken cancellationToken)
    {

        var currency = await currencyService.FindByIdAsync(cancellationToken, request.Id);
        if (currency == null)
            return new DeleteCurrencyCommandRes { Success = false, Message = "Currency not found" };

        await currencyService.BulkRemove(p => p.Id == request.Id, cancellationToken);
        return new DeleteCurrencyCommandRes { Success = true };
    }
}