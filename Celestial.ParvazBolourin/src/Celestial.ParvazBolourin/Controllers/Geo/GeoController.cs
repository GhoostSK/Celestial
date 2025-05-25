using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Api.Controllers;
using Celestial.ParvazBolourin.Service.Command.Geo;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Geo;

public class GeoController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> CountryList(CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetCountryListCommandReq(), cancellationToken);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> StateList([Required] int countryId, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetStateListCommandReq { CountryId = countryId }, cancellationToken);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> CityList([Required] int stateId, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetCityListCommandReq { CountryId = stateId }, cancellationToken);

        return Ok(data);
    }
}