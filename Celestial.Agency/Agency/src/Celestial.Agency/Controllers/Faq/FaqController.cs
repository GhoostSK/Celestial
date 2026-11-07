using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Faq;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Faq;

public class FaqController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetFaqList([FromQuery] GetFaqListReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetFaqListCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}
