using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Service.Command.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Geo;

public class GetCityListCommandHandler(ICityService cityService)
    : IRequestHandler<GetCityListCommandReq, GetCityListCommandRes>
{
    public async Task<GetCityListCommandRes> Handle(GetCityListCommandReq request, CancellationToken cancellationToken)
    {
        var data = await cityService.GetCityList(request.StateId, cancellationToken);
        return new GetCityListCommandRes
        {
            Items = data
        };
    }
}