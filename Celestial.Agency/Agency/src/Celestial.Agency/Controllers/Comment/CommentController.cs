using System.Security.Claims;
using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Comment;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Comment;

public class CommentController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddComment(AddCommentReqDto request,
        CancellationToken cancellationToken)
    {
        var data = request.Adapt<AddCommentCommandReq>();
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return BadRequest("Please Login");

        data.UserId = userId!;

        var response = await Mediator.Send(data, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCommentBySlug(GetCommentBySlugReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetCommentsBySlugCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}