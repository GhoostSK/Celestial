using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Tour;

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

    [HttpGet]
    public async Task<IActionResult> GetTourById([FromQuery] GetTourByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new GetTourByIdCommandReq { LanguageId = request.LanguageId, TourId = request.TourId, Slug = request.Slug },
                cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetFeaturedTours([FromQuery] GetTourFeaturedRequestDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetTourFeaturedCommandReq { LanguageId = request.LanguageId, TopCount = request.TopCount }, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}