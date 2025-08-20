using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Blog.BlogTag;

public class BlogTagController : Api.Controllers.BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBlogTagCommandReq request)
    {
        var response = await Mediator.Send(request);
        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBlogTagCommandReq request)
    {
        var response = await Mediator.Send(request);
        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await Mediator.Send(new DeleteBlogTagCommandReq { Id = id });
        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }
}