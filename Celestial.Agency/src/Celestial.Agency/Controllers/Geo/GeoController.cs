using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Service.Command.Geo;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Geo;

public class GeoController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> CountryList(CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetCountryListCommandReq(), cancellationToken);
        return Ok(data);
    }

    //[HttpGet]
    //public async Task<IActionResult> StateList([Required] int countryId, CancellationToken cancellationToken)
    //{
    //    var data = await Mediator.Send(new GetStateListCommandReq { CountryId = countryId }, cancellationToken);
    //    return Ok(data);
    //}

    [HttpGet]
    public async Task<IActionResult> CityList([Required] int countryId, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetCityListCommandReq { CountryId = countryId }, cancellationToken);

        return Ok(data);
    }
}