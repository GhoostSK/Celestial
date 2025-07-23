using Celestial.ParvazBolourin.Api.Controllers;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using Celestial.Service.Command.Exchange;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Controllers.Exchange;

public class CurrencyController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCurrenciesList(CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(new GetCurrenciesListCommandReq(), cancellationToken);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrencyById(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrencyByIdCommandReq { Id = id }, cancellationToken);
        if (result == null)
            return NotFound(new { message = "Currency not found" });

        return Ok(result);
    }
}