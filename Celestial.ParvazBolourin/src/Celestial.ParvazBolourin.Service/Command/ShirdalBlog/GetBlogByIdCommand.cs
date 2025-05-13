using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog;

public sealed class GetBlogByIdCommandReq : BaseEntity, IRequest<GetBlogByIdCommandRes>
{
    public int LanguageId { get; set; }
    public int Id { get; set; }
}

public sealed class GetBlogByIdCommandRes : ServiceResContextBase
{
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public int Likes { get; set; }
    public List<BlogCategoryDto> BlogCategories { get; set; } = new();
 
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogCategoryDto
{
    public long Id { get; set; }
}