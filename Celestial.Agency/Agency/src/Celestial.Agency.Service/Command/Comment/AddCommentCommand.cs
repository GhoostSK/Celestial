using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Comment;

public sealed class AddCommentCommandReq : IRequest<AddCommentCommandRes>
{
    public CommentKeyGroupType KeyGroup { get; set; }

    /// <summary>
    ///     اسلاگ موجودیت (مقاله، تور یا هتل) که نظر برای آن ثبت می‌شود.
    ///     در صورت پر بودن، <see cref="EntityId" /> از روی آن محاسبه می‌شود.
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    public long EntityId { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public long? ParentCommentId { get; set; }
}

public sealed class AddCommentCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
}
