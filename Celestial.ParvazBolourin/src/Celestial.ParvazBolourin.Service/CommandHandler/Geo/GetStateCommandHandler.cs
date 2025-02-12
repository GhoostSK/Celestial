using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Service.Command.Geo;
using Mapster;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Geo;

public class GetStateCommandHandler(IStateService stateService)
    : IRequestHandler<GetStateCommandReq, GetStateCommandRes>
{
    public async Task<GetStateCommandRes> Handle(GetStateCommandReq request, CancellationToken cancellationToken)
    {
        var data = await stateService.GetStateList(request.CountryId, cancellationToken);
        return new()
        {
            Items = data
        };
    }
}