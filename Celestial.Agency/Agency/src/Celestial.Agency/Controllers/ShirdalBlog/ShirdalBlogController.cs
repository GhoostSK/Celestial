using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.ShirdalBlog;

public class ShirdalBlogController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBlogsList([FromQuery] GetBlogsListReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetBlogsListCommandReq
        {
            Language = request.Language,
            CategoriesId = request.CategoriesId,
            TagsId = request.TagsId,
            Title = request.Title,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            PopularTagCount = request.PopularTagsCount
        }, cancellationToken);
        var categoriesList = await Mediator.Send(new GetBlogCategoryListCommandReq
        {
            Language = request.Language
        }, cancellationToken);
        response.Categories = categoriesList.Items;
        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBlogById([FromQuery] GetBlogsByIdReqDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new GetBlogCommandReq { Slug = request.Slug },
                cancellationToken);

        if (response.HasError) return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetTopBlogs([FromQuery] int topCount, [FromRoute] string language,
        CancellationToken cancellationToken)
    {
        var command = new GetTopBlogsCommandReq
        {
            TopCount = topCount,
            Language = language
        };

        var result = await Mediator.Send(command, cancellationToken);

        if (result.HasError)
            return BadRequest(result.Error);

        return Ok(result.Items);
    }
}