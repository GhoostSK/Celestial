using Celestial.ParvazBolourin.Api.Controllers;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Controllers.ShirdalBlog.BlogCategory;

public class ShirdalBlogCategoryController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogCategoryByIdReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetBlogCategoryByIdCommandReq
        {
            Id = request.Id,
            Language = request.Language,
            Slug = request.slug
        }, cancellationToken);

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