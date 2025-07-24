using Celestial.Agency.Service.Command.Contact;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Contact;

public class ContactUsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetContactUsList(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetContactUsListCommandReq(), cancellationToken);

        if (result.HasError) return BadRequest(result.Error);

        return Ok(result.ContactUsList);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveContactUs(long contactUsId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new RemoveContactUsCommandReq
        {
            ContactUsId = contactUsId
        }, cancellationToken);

        if (result.HasError) return BadRequest(result.Error);

        return Ok("Contact removed successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetContactUsById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetContactUsByIdCommandReq
        {
            Id = id
        }, cancellationToken);

        if (result.HasError) return BadRequest(result.Error);

        if (result.Item == null) return NotFound($"ContactUs with ID {id} not found.");

        return Ok(result.Item);
    }
}