using Celestial.Api.Models.Dto;
using Celestial.ParvazBolourin.Api.Controllers;
using Celestial.ParvazBolourin.Service.Command.Tour.TourEquipment;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Controllers.Tour.TourEquipment;

public class TourEquipmentController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetEquipmentList([FromRoute]string language)
    {
        var response = await Mediator.Send(new GetTourEquipmentListCommandReq()
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
        var response = await Mediator.Send(new GetTourEquipmentByIdCommandReq()
        {
            Id = request.Id,
            Language = request.Language,

        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}