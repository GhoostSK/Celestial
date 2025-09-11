using Celestial.Agency.Core.DAL.Model.Global;

namespace Celestial.Agency.Api.Models.Dto;

public class AddCommentReqDto
{
    public CommentKeyGroupType KeyGroup { get; set; }
    public int EntityId { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public long? ParentCommentId { get; set; }
}

public sealed class GetCommentByEntityIdReqDto
{
    public CommentKeyGroupType KeyGroup { get; set; }
    public int EntityId { get; set; }
}