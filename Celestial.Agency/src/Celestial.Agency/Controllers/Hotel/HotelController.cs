using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Hotel;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Hotel;

public class HotelController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetHotelsList([FromQuery] GetHotelListReqDto request,
        CancellationToken cancellationToken)
    {
        var model = request.Adapt<GetHotelsListCommandReq>();
        var response =
            await Mediator.Send(model, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetHotel([FromQuery] GetHotelReqDto request, CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(
                new GetHotelCommandReq { Slug = request.Slug },
                cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}