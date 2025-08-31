using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Blog.BlogTag;

public class BlogTagController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogTagByIdCommandReq request)
    {
        var response = await Mediator.Send(new GetBlogTagByIdCommandReq
        {
            Id = request.Id,
            Language = request.Language,
            Slug = request.Slug
        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromRoute] string language)
    {
        var response = await Mediator.Send(new GetBlogTagListCommandReq
        {
            Language = language
        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}