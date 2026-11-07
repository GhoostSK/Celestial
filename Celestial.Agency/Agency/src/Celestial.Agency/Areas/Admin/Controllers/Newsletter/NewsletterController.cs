using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Newsletter;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Newsletter;

public class NewsletterController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetSubscriptionList([FromQuery] GetNewsletterSubscriptionListReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(request.Adapt<GetNewsletterSubscriptionListCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveSubscription([FromQuery] UnsubscribeNewsletterReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<UnsubscribeNewsletterCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok("Subscription removed successfully.");
    }
}
