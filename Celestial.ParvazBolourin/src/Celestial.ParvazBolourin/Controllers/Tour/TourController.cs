using Celestial.Api.Models.Dto;
using Celestial.ParvazBolourin.Api.Controllers;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Celestial.Service.Command.Tour;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Controllers.Tour;

public class TourController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetToursList([FromQuery] GetToursListReqDto request,
        CancellationToken cancellationToken)
    {
        var model = request.Adapt<GetToursListCommandReq>();
        var response =
            await Mediator.Send(model, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetTour([FromQuery] GetTourByIdReqDto request, CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new GetTourCommandReq { Slug = request.Slug },
                cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetFeaturedTours([FromQuery] GetTourFeaturedRequestDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetTourFeaturedCommandReq { Language = request.Language, TopCount = request.TopCount }, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}