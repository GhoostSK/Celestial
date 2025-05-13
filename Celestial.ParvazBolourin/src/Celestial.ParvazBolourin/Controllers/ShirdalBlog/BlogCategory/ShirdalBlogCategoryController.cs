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
    public async Task<IActionResult> GetById([FromQuery] long id, [FromQuery] int languageId)
    {
        var result = await Mediator.Send(new GetBlogCategoryByIdCommandReq
        {
            Id = id,
            LanguageId = languageId
        });

        if (result == null)
            return NotFound("BlogCategory not found");

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int languageId)
    {
        var result = await Mediator.Send(new GetBlogCategoryListCommandReq
        {
            LanguageId = languageId
        });

        return Ok(result);
    }
}