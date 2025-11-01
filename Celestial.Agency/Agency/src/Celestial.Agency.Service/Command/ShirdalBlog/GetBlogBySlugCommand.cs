using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class GetBlogBySlugCommandReq : BaseEntity, IRequest<GetBlogBySlugCommandRes>
{
    public string? Slug { get; set; }
    public string Language { get; set; } = string.Empty;
}

public sealed class GetBlogBySlugCommandRes : ServiceResContextBase
{
    public BlogItem Item { get; set; } = new();
}

public sealed class BlogItem
{
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public int Likes { get; set; }
    public BlogCategoryDto Category { get; set; } = new();
    public List<BlogTagDto> Tags { get; set; } = new();
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;
}

public sealed class BlogCategoryDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogTagDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}