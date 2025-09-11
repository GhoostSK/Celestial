using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Service.Command.localization;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Controllers.Blog;

public class BlogController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBlogsList([FromQuery] GetBlogsListReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetBlogsListCommandReq>(), cancellationToken);

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
    public async Task<IActionResult> GetBlogBySlug([FromQuery] GetBlogBySlugReqDto request,
        CancellationToken cancellationToken)
    {
        var slugModel = request.Adapt<GetSlugCommandReq>();
        slugModel.KeyGroup = LocalKeyGroupType.Blog;

        var slugSystem = await Mediator.Send(slugModel, cancellationToken);

        if (!slugSystem.IsCorrect) return RedirectToAction("GetBlogBySlug", new { slugSystem.Slug, request.Language });
        var response =
            await Mediator.Send(request.Adapt<GetBlogBySlugCommandReq>(),
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
        var response = await Mediator.Send(command, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetLatestBlog(GetLatestBlogsReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetLatestBlogListCommandReq>(), cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}