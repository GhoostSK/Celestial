using Celestial.Agency.Api.Areas.Admin.Models.Dto;
using Celestial.Agency.Service.Command.Comment;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Comment;

public class CommentController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> ConfirmComment(ConfirmCommentReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<ConfirmCommentCommandReq>(), cancellationToken);
        if (response.HasError) return BadRequest(response.Error);
        return Ok("comment confirmed successfully");
    }
}