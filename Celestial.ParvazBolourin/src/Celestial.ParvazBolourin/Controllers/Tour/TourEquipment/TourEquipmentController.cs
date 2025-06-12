using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Tour.TourEquipment;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Tour.TourEquipment;

public class TourEquipmentController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetEquipmentList(int languageId)
    {
        var response = await Mediator.Send(new GetTourEquipmentListCommandReq()
        {
            LanguageId = languageId
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
            LanguageId = request.LanguageId,

        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}