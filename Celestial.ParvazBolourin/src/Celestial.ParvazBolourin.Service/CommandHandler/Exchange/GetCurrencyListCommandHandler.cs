using Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Exchange;


class GetCurrencyListCommandHandler(ICurrencyService currencyService) : IRequestHandler<GetCurrenciesListCommandReq, GetCurrenciesListCommandRes>
{
    public async Task<GetCurrenciesListCommandRes> Handle(GetCurrenciesListCommandReq request, CancellationToken cancellationToken)
    {
        var data = await currencyService.GetCurrencyList(cancellationToken);
        return new()
        {
            Items = data
        };
    }
}