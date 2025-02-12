using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using Celestial.ParvazBolourin.Service.Command.Geo;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Celestial.ParvazBolourin.Api.Controllers.Geo;

public class GeoController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> CountryList(CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetCountryCommandReq(), cancellationToken);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> StateList(int countryId, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetStateCommandReq { CountryId = countryId }, cancellationToken);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> CityList(int stateId, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetCityCommandReq { StateId = stateId }, cancellationToken);

        return Ok(data);
    }
}