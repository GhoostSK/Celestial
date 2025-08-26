using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Blog.BlogCategory;

public class CategoryController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogCategoryByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetBlogCategoryByIdCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromRoute] string language, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetBlogCategoryListCommandReq
        {
            Language = language
        }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}