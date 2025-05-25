using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.ShirdalBlog.BlogCategory;

public class ShirdalBlogCategoryController : BaseController
{
    private readonly IMediator _mediator;

    public ShirdalBlogCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogCategoryByIdReqDto request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBlogCategoryByIdCommandReq
        {
            Id = request.Id,
            LanguageId = request.LanguageId,
            Slug = request.slug
        },cancellationToken);

        if (result == null)
            return NotFound("BlogCategory not found");

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int languageId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBlogCategoryListCommandReq
        {
            LanguageId = languageId
        }, cancellationToken);

        return Ok(result);
    }
}