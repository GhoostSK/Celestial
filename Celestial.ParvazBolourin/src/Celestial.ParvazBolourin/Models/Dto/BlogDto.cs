namespace Celestial.ParvazBolourin.Api.Models.Dto;

public sealed class AddBlogReqDto
{
    public List<BlogLocalizedProperty> BlogLocalizedProperties { get; set; } = new();
    public string PicturePath { get; set; } = string.Empty;
}

public sealed class BlogLocalizedProperty
{
    public int LanguageId { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string HeaderText { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
}

public sealed class GetBlogsListReqDto
{
    public int LanguageId { get; set; }
}

public sealed class RemoveBlogsReqDto
{
    public int BlogId { get; set; }
}

public sealed class GetBlogsByIdReqDto
{
    public int LanguageId { get; set; }
    public int Id { get; set; }
}