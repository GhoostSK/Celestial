using Azure.Core;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.ShirdalBlog.BlogTag;

public class ShirdalBlogTagController : BaseController
{
    private readonly IMediator _mediator;

    public ShirdalBlogTagController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogTagByIdCommandReq request)
    {
        var result = await Mediator.Send(new GetBlogTagByIdCommandReq
        {
            Id = request.Id,
            LanguageId = request.LanguageId,
            Slug=request.Slug,
        });

        if (result == null)
            return NotFound("BlogCategory not found");

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int languageId)
    {
        var result = await Mediator.Send(new GetBlogTagListCommandReq
        {
            LanguageId = languageId,

        });
        return Ok(result);
    }
}