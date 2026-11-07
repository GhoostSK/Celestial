using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Newsletter;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Newsletter;

public class NewsletterController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeNewsletterReqDto request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await Mediator.Send(request.Adapt<SubscribeNewsletterCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Unsubscribe([FromBody] UnsubscribeNewsletterReqDto request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await Mediator.Send(request.Adapt<UnsubscribeNewsletterCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}
