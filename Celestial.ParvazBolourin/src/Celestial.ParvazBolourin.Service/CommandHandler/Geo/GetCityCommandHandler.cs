using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Service.Command.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Geo;

public class GetCityCommandHandler(ICityService cityService)
    : IRequestHandler<GetCityCommandReq, GetCityCommandRes>
{
    public async Task<GetCityCommandRes> Handle(GetCityCommandReq request, CancellationToken cancellationToken)
    {
        var data = await cityService.GetCityList(request.StateId, cancellationToken);
        return new GetCityCommandRes
        {
            Items = data
        };
    }
}