using Celestial.ParvazBolourin.Api.Areas.Admin.Controllers;
using Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Exchange;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Areas.Admin.Controllers.Exchange;

public class CurrencyController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddCurrency([FromBody] AddCurrencyDto request, CancellationToken cancellationToken)
    {
        var model = request.Adapt<AddCurrencyCommandReq>();
        var result = await Mediator.Send(model, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> EditCurrency([FromBody] EditCurrencyDto request, CancellationToken cancellationToken)
    {
        var model = request.Adapt<EditCurrencyCommandReq>();
        var result = await Mediator.Send(model, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCurrency(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteCurrencyCommandReq { Id = id }, cancellationToken);

        if (!result.Success)
            return NotFound(new { message = result.Message });

        return Ok(new { message = "Currency deleted successfully" });
    }

}