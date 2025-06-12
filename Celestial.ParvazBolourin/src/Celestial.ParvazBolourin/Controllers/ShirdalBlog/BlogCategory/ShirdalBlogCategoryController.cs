using Azure;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.ShirdalBlog.BlogCategory;

public class ShirdalBlogCategoryController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetBlogCategoryByIdReqDto request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetBlogCategoryByIdCommandReq
        {
            Id = request.Id,
            LanguageId = request.LanguageId,
            Slug = request.slug
        }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int languageId, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetBlogCategoryListCommandReq
        {
            LanguageId = languageId
        }, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}