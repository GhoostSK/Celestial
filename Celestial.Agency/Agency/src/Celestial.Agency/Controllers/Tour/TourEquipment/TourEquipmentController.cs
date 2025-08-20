using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Tour.TourEquipment;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Tour.TourEquipment;

public class TourEquipmentController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetEquipmentList([FromRoute] string language)
    {
        var response = await Mediator.Send(new GetTourEquipmentListCommandReq
        {
            Language = language
        });
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetEquipmentById request)
    {
        var response = await Mediator.Send(new GetTourEquipmentByIdCommandReq
        {
            Id = request.Id,
            Language = request.Language
        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}