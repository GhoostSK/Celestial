using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Tour;

public class TourController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> GetToursList([FromBody] GetToursListReqDto request,
        CancellationToken cancellationToken)
    {
        var model = request.Adapt<GetToursListCommandReq>();
        var response =
            await Mediator.Send(model, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetTourById([FromQuery] GetTourByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new GetTourByIdCommandReq { LanguageId = request.LanguageId, TourId = request.TourId },
                cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}