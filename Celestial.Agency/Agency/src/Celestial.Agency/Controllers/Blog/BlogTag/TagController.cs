using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Blog.BlogTag;

public class BlogTagController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogTagBySlugReqDto request)
    {
        var response = await Mediator.Send(request.Adapt<GetBlogTagBySlugCommandReq>());

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetTagListReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetBlogTagListCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}