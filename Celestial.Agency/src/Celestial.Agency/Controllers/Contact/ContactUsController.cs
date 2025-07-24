using Celestial.Agency.Api.Identity.Model;
using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Contact;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Contact;

public class ContactUsController(UserManager<User> userManager) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddContactUs([FromBody] AddContactUsReqDto request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Empty;

        if (User.Identity.IsAuthenticated)
        {
            var user = await userManager.GetUserAsync(User);
            userId = Guid.Parse(user!.Id);
        }

        var command = new AddContactUsCommandReq
        {
            Name = request.Name,
            Email = request.Email,
            Message = request.Message,
            UserId = userId
        };

        var response = await Mediator.Send(command, cancellationToken);

        if (response.HasError)
            return StatusCode(StatusCodes.Status500InternalServerError, Response);

        return Ok(response);
    }
}