using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.ShirdalBlog;

public class ShirdalBlogController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBlogsList([FromQuery] GetBlogsListReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetBlogsListCommandReq { LanguageId = request.LanguageId },
            cancellationToken);

        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogsId([FromQuery] GetBlogsByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new GetBlogByIdCommandReq { LanguageId = request.LanguageId, Id = request.Id },
                cancellationToken);

        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }
}