using Celestial.Agency.Core.DAL.Repositories.Exchange;
using Celestial.Agency.Service.Command.Exchange;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Exchange;

public class DeleteCurrencyCommandHandler(ICurrencyService currencyService)
    : IRequestHandler<DeleteCurrencyCommandReq, DeleteCurrencyCommandRes>
{
    public async Task<DeleteCurrencyCommandRes> Handle(DeleteCurrencyCommandReq request,
        CancellationToken cancellationToken)
    {
        var currency = await currencyService.FindByIdAsync(cancellationToken, request.Id);
        if (currency == null)
            return new DeleteCurrencyCommandRes { Success = false, Message = "Currency not found" };

        await currencyService.BulkRemove(p => p.Id == request.Id, cancellationToken);
        return new DeleteCurrencyCommandRes { Success = true };
    }
}