using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Hotel;

public class HotelController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetHotelsList([FromQuery] GetHotelListReqDto request, CancellationToken cancellationToken)
    {
        var model = request.Adapt<GetHotelsListCommandReq>();
        var response =
            await Mediator.Send(model, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetHotelById([FromQuery] HotelByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(
                new GetHotelByIdCommandReq { LanguageId = request.LanguageId, HotelId = request.HotelId },
                cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}