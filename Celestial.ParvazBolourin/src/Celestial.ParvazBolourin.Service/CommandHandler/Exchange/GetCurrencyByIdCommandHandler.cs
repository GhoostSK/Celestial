using Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Exchange;

public class GetCurrencyByIdCommandHandler : IRequestHandler<GetCurrencyByIdCommandReq, GetCurrencyByIdCommandRes>
{
    private readonly ICurrencyService _currencyService;

    public GetCurrencyByIdCommandHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task<GetCurrencyByIdCommandRes> Handle(GetCurrencyByIdCommandReq request, CancellationToken cancellationToken)
    {
        var currency = await _currencyService.FindByIdAsync(cancellationToken, request.Id);
        if (currency == null)
            return null!;

        return new GetCurrencyByIdCommandRes
        {
            Id = currency.Id,
            Name = currency.Name,
            ExchangeRate = currency.ExchangeRate
        };
    }
}