using Celestial.ParvazBolourin.Api.Identity.Model;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.Contact;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Contact;

public class ContactUsController : BaseController
{
    private readonly UserManager<User> _userManager;

    public ContactUsController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddContactUs([FromBody] AddContactUsReqDto request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Empty;

        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
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