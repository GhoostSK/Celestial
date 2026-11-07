using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Faq;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Faq;

public class FaqController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetFaqList([FromQuery] string language, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetFaqListCommandReq
        {
            Language = language,
            OnlyPublished = false
        }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddFaq([FromBody] AddFaqReqDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await Mediator.Send(request.Adapt<AddFaqCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFaq([FromBody] UpdateFaqReqDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await Mediator.Send(request.Adapt<UpdateFaqCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok("Faq updated successfully.");
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFaq([FromQuery] RemoveFaqReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<RemoveFaqCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok("Faq removed successfully.");
    }
}
