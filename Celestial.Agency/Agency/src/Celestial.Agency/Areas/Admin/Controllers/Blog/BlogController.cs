using Celestial.Agency.Api.Areas.Admin.Models.Dto;
using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Blog;

public class BlogController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddBlog(AddBlogReqDto request, CancellationToken cancellationToken)
    {
        var model = request.Adapt<AddBlogCommandReq>();
        var response = await Mediator.Send(model, cancellationToken);

        if (response.HasError) return BadRequest(response.Error);
        return Ok("Blog Added Successfully");
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveBlog([FromQuery] RemoveBlogsReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new RemoveBlogCommandReq { Id = request.BlogId }, cancellationToken);

        if (response.HasError) return BadRequest(response.Error);
        return Ok("Blog deleted successfully");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new UpdateBlogCommandReq
        {
            Id = request.Id,
            BlogLocalizedProperties = request.BlogLocalizedProperties,
            PicturePath = request.PicturePath
        }, cancellationToken);

        if (response.HasError) return BadRequest(response.Error);
        return Ok("Blog updated successfully");
    }
}