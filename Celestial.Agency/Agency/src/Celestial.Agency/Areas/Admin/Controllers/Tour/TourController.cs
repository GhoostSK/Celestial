using Celestial.Agency.Api.Areas.Admin.Models.Dto;
using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Tour;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Tour;

public class TourController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddTour(AddTourReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<AddTourCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveTour([FromQuery] RemoveTourByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(request.Adapt<RemoveTourCommandReq>(), cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTour([FromBody] UpdateTourDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<UpdateTourCommandReq>(), cancellationToken);

        if (response.HasError) return BadRequest(response.Error);
        return Ok("Tour updated successfully");
    }
}