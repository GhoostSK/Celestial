using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Controllers;

[Route("api/Admin/[controller]/[action]")]
//[Authorize(Policy = "Admin")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}