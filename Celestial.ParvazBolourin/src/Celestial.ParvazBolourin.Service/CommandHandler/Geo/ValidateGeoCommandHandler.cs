using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Service.Command.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Geo;

public class ValidateGeoCommandHandler(IStateService stateService, ICityService cityService, ICountryService countryService) : IRequestHandler<ValidateGeoCommandReq, bool>
{
    public async Task<bool> Handle(ValidateGeoCommandReq request, CancellationToken cancellationToken)
    {
        var country = await countryService.FindByIdAsync(cancellationToken, request.CountryId);
        if (country == null)
            return false;
        var state = await stateService.FirstOrDefaultAsync(p =>
            p.Id == request.StateId && p.Country_id == request.CountryId, cancellationToken: cancellationToken);
        if (state == null)
            return false;
        var city = await cityService.FirstOrDefaultAsync(p =>
            p.Id == request.CityId && p.State_id == request.StateId, cancellationToken: cancellationToken);
        if (city == null)
            return false;

        return true;
    }
}