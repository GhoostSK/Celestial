using Celestial.Agency.Core.DAL.Model.Global;

namespace Celestial.Agency.Api.Models.Dto;

public class AddCommentReqDto
{
    public CommentKeyGroupType KeyGroup { get; set; }
    public string Slug { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public long? ParentCommentId { get; set; }
}

public sealed class GetCommentBySlugReqDto
{
    public required string Slug { get; init; }
    public required LocalKeyGroupType LocalGroup { get; set; }
}