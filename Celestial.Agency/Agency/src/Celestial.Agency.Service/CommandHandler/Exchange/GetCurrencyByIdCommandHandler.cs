using Celestial.Agency.Core.DAL.Repositories.Exchange;
using Celestial.Agency.Service.Command.Exchange;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Exchange;

public class GetCurrencyByIdCommandHandler : IRequestHandler<GetCurrencyByIdCommandReq, GetCurrencyByIdCommandRes>
{
    private readonly ICurrencyService _currencyService;

    public GetCurrencyByIdCommandHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task<GetCurrencyByIdCommandRes> Handle(GetCurrencyByIdCommandReq request,
        CancellationToken cancellationToken)
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