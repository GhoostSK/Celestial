using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Controllers.ShirdalBlog.BlogTag;

public class ShirdalBlogTagController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBlogTagCommandReq request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBlogTagCommandReq request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await Mediator.Send(new DeleteBlogTagCommandReq { Id = id });
        return Ok(result);
    }
}