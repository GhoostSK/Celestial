using Celestial.ParvazBolourin.Api.Controllers;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.Service.Command.Visa;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Controllers.Visa;

public class VisaController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetVisaList([FromQuery] GetVisaListReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetVisaListCommandReq { Language = request.Language, PageNumber = request.PageNumber, PageSize = request.PageSize },
            cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetVisaById([FromQuery] GetVisaByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new GetVisaByIdCommandReq {Slug =request.Slug },
                cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}