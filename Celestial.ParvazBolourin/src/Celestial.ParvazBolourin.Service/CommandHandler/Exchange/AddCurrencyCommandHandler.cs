using Celestial.ParvazBolourin.Core.DAL.Model.Exchange;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Exchange;

public class AddCurrencyCommandHandler : IRequestHandler<AddCurrencyCommandReq, AddCurrencyCommandRes>
{
    private readonly ICurrencyService _currencyService;

    public AddCurrencyCommandHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task<AddCurrencyCommandRes> Handle(AddCurrencyCommandReq request, CancellationToken cancellationToken)
    {
        var currency = new Currency
        {
            Name = request.Name,
            ExchangeRate = request.ExchangeRate
        };

        await _currencyService.AddAsync(currency, cancellationToken);
        await _currencyService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddCurrencyCommandRes { };
    }
}