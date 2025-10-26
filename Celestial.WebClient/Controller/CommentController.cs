using System.Globalization;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.WebClient.Controller;

public sealed class CommentsController(ICommentService commentService) : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment([FromForm] AddCommentReqDto request, [FromForm] string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(request.Slug) ||
            string.IsNullOrWhiteSpace(request.CommentText))
        {
            TempData["CommentError"] = "لطفاً پیام و شناسه مطلب را وارد کنید.";
            return LocalRedirect(returnUrl ?? "/");
        }

        // اگر Language نیامد، از culture فعلی استفاده کن
        request.Language ??= CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        var res = await commentService.AddCommentAsync(request);

        if (res.HasError)
            TempData["CommentError"] = string.IsNullOrWhiteSpace(res.ErrorMessage)
                ? "خطا در ثبت نظر."
                : res.ErrorMessage!;
        else
            TempData["CommentSuccess"] = "نظر شما ثبت شد و پس از تأیید نمایش داده می‌شود.";

        return LocalRedirect(returnUrl ?? $"/{request.Language}/Blog/{request.Slug}");
    }
}