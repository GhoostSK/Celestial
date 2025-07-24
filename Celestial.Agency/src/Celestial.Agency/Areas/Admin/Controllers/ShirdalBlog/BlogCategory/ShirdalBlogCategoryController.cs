using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.ShirdalBlog.BlogCategory;

public class ShirdalBlogCategoryController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBlogCategoryCommandReq request)
    {
        var response = await Mediator.Send(request);
        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBlogCategoryCommandReq request)
    {
        var response = await Mediator.Send(request);
        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await Mediator.Send(new DeleteBlogCategoryCommandReq { Id = id });
        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }
}