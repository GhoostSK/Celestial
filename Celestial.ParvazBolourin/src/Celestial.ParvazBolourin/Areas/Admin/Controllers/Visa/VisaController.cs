using Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;
using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using Celestial.ParvazBolourin.Service.Command.Visa;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Controllers.Visa;

public class VisaController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddVisa([FromBody] AddVisaReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new AddVisaCommandReq() { Visa = request.Adapt<Visas>(), VisaLocalizedProperty = request.VisaLocalizedProperty }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveVisa([FromQuery] RemoveVisaReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new RemoveVisaCommandReq() { Id = request.Id }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVisa([FromBody] UpdateVisaDtoReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new UpdateVisaCommandReq() 
        { 
            Id = request.Id, 
            Visa = request.Visa, 
            VisaLocalizedProperty = request.VisaLocalizedProperty 
        }, cancellationToken);

        if (response.HasError)
        {
            return BadRequest(response.Error);
        }
        return Ok("Visa updated successfully");
    }
}

