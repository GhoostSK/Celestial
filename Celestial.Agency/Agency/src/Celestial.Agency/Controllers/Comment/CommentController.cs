using System.Security.Claims;
using Celestial.Agency.Api.Identity.Model;
using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.Comment;
using Celestial.SharedKernel.Common;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.Api.Controllers.Comment;

public class CommentController(UserManager<User> userManager) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddComment(AddCommentReqDto request,
        CancellationToken cancellationToken)
    {
        var data = request.Adapt<AddCommentCommandReq>();
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized(new ValidationError("C401", "Please login to post a comment."));

        data.UserId = userId!;

        var response = await Mediator.Send(data, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCommentBySlug([FromQuery] GetCommentBySlugReqDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request.Adapt<GetCommentsBySlugCommandReq>(), cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        await FillUserNamesAsync(response.Items, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    ///     نام کاربری نویسنده‌ی نظرها از Identity خوانده می‌شود؛ لایه‌ی Service به آن دسترسی ندارد.
    /// </summary>
    private async Task FillUserNamesAsync(List<CommentItemDto> comments, CancellationToken cancellationToken)
    {
        var allComments = comments.Concat(comments.SelectMany(c => c.Replies)).ToList();

        var userIds = allComments
            .Select(c => c.UserId)
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct()
            .ToList();

        if (!userIds.Any())
            return;

        var userNames = await userManager.Users
            .Where(u => userIds.Contains(u.Id))
            .Select(u => new { u.Id, u.UserName })
            .ToDictionaryAsync(u => u.Id, u => u.UserName ?? string.Empty, cancellationToken);

        foreach (var comment in allComments)
            comment.UserName = userNames.TryGetValue(comment.UserId, out var name) ? name : string.Empty;
    }
}