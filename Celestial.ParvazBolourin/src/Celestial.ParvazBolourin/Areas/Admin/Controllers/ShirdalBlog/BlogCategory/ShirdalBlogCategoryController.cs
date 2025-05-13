using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Controllers.ShirdalBlog.BlogCategory;

public class ShirdalBlogCategoryController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBlogCategoryCommandReq request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBlogCategoryCommandReq request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await Mediator.Send(new DeleteBlogCategoryCommandReq { Id = id });
        return Ok(result);
    }

}