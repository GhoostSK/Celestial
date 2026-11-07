using System.Security.Claims;
using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Booking;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Booking;

public class BookingController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddTourBooking([FromBody] AddTourBookingReqDto request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = request.Adapt<AddTourBookingCommandReq>();
        command.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        var response = await Mediator.Send(command, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetBookingByCode([FromQuery] GetTourBookingByCodeReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetTourBookingByCodeCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}
