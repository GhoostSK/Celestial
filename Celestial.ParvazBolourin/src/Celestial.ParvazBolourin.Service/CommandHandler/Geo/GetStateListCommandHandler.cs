using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Service.Command.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Geo;

public class GetStateListCommandHandler(IStateService stateService)
    : IRequestHandler<GetStateListCommandReq, GetStateListCommandRes>
{
    public async Task<GetStateListCommandRes> Handle(GetStateListCommandReq request, CancellationToken cancellationToken)
    {
        var data = await stateService.GetStateList(request.CountryId, cancellationToken);
        return new GetStateListCommandRes
        {
            Items = data
        };
    }
}