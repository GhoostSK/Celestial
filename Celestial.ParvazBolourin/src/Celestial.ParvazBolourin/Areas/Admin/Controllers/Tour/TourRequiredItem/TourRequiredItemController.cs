using Celestial.Api.Models.Dto;
using Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour.TourEquipment;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Controllers.Tour.TourRequiredItem;

public class TourRequiredItemController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add(AddTourEquipmentDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<AddTourEquipmentCommandReq>(), cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromQuery] RemoveTourRequiredItemByIdReqDto request, CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new RemoveTourCommandReq { Id = request.TourId }, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetTourByIdRequiredItemReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetTourEquipmentByIdCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] string language)
    {
        var response = await Mediator.Send(new GetTourEquipmentListCommandReq
        {
            Language = language,

        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    //[HttpPut]
    //public async Task<IActionResult> UpdateTour([FromBody] UpdateTourDto request, CancellationToken cancellationToken)
    //{
    //    var response = await Mediator.Send(request.Adapt<UpdateTourCommandReq>(), cancellationToken);

    //    if (response.HasError) return BadRequest(response.Error);
    //    return Ok("Tour updated successfully");
    //}
}