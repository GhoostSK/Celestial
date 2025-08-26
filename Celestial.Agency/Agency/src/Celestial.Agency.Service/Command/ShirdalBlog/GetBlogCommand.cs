using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class GetBlogCommandReq : BaseEntity, IRequest<GetBlogByIdCommandRes>
{
    public string? Slug { get; set; }
}

public sealed class GetBlogByIdCommandRes : ServiceResContextBase
{
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public int Likes { get; set; }
    public BlogCategoryDto BlogCategory { get; set; } = new();
    public List<BlogTagDto> BlogTags { get; set; } = new();
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