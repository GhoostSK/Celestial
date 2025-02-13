using Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Exchange;

public class EditCurrencyCommandHandler : IRequestHandler<EditCurrencyCommandReq, EditCurrencyCommandRes>
{
    private readonly ICurrencyService _currencyService;

    public EditCurrencyCommandHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task<EditCurrencyCommandRes> Handle(EditCurrencyCommandReq request, CancellationToken cancellationToken)
    {

        var currency = await _currencyService.FindByIdAsync(cancellationToken, request.Id);
        currency.ExchangeRate = request.ExchangeRate;
        currency.Name = request.Name;
        await _currencyService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new EditCurrencyCommandRes { };
    }
}