using Celestial.Api.Models.Dto;
using Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Celestial.Service.Command.Tour;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Controllers.Tour;

public class TourController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddTour(AddTourReqDto request, CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(
                new AddTourCommandReq
                {
                    Tours = request.Tours,
                    LocalizedProperty = request.LocalizedProperty.Adapt<List<TourLocalizedPropertyItem>>()
                }, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveTour([FromQuery] RemoveTourByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new RemoveTourCommandReq { Id = request.TourId }, cancellationToken);
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