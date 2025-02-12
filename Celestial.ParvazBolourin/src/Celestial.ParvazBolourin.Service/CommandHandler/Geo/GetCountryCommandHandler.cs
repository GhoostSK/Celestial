using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Service.Command.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Geo;

public class GetCountryCommandHandler(ICountryService countryService)
    : IRequestHandler<GetCountryCommandReq, GetCountryCommandRes>
{
    public async Task<GetCountryCommandRes> Handle(GetCountryCommandReq request, CancellationToken cancellationToken)
    {
        var data = await countryService.GetCountryList(cancellationToken);
        return new GetCountryCommandRes
        {
            Items = data
        };
    }
}