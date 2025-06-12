using Azure;
using Azure.Core;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.ShirdalBlog.BlogTag;

public class ShirdalBlogTagController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogTagByIdCommandReq request)
    {
        var response = await Mediator.Send(new GetBlogTagByIdCommandReq
        {
            Id = request.Id,
            LanguageId = request.LanguageId,
            Slug = request.Slug,
        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int languageId)
    {
        var response = await Mediator.Send(new GetBlogTagListCommandReq
        {
            LanguageId = languageId,

        });

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }
}