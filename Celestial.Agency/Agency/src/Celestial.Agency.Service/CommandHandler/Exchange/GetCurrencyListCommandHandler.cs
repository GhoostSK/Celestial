using Celestial.Agency.Core.DAL.Repositories.Exchange;
using Celestial.Agency.Service.Command.Exchange;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Exchange;

internal class GetCurrencyListCommandHandler(ICurrencyService currencyService)
    : IRequestHandler<GetCurrenciesListCommandReq, GetCurrenciesListCommandRes>
{
    public async Task<GetCurrenciesListCommandRes> Handle(GetCurrenciesListCommandReq request,
        CancellationToken cancellationToken)
    {
        var data = await currencyService.GetCurrencyList(cancellationToken);
        return new GetCurrenciesListCommandRes
        {
            Items = data
        };
    }
}