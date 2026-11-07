using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Booking;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Booking;

public class BookingController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBookingList([FromQuery] GetTourBookingListReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetTourBookingListCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBookingStatus([FromBody] UpdateTourBookingStatusReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<UpdateTourBookingStatusCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok("Booking status updated successfully.");
    }
}
