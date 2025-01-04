using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Visa;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Visa;

public class VisaController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetVisaList([FromQuery] GetVisaListReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetVisaListCommandReq() { LanguageId = request.LanguageId }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetVisaById([FromQuery] GetVisaByIdReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetVisaByIdCommandReq() { Id = request.Id, LanguageId = request.LanguageId }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}

