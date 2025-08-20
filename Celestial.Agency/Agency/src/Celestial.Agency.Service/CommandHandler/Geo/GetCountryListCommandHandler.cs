using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.Service.Command.Geo;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Geo;

public class GetCountryListCommandHandler(ICountryService countryService)
    : IRequestHandler<GetCountryListCommandReq, GetCountryListCommandRes>
{
    public async Task<GetCountryListCommandRes> Handle(GetCountryListCommandReq request,
        CancellationToken cancellationToken)
    {
        var data = await countryService.GetCountryList(cancellationToken);
        return new GetCountryListCommandRes
        {
            Items = data
        };
    }
}