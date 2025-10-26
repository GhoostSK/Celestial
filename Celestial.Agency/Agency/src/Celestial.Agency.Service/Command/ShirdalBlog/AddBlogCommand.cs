using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class AddBlogCommandReq : BaseEntity, IRequest<AddBlogCommandRes>
{
    public List<BlogLocalizedProperty> BlogLocalizedProperties { get; set; } = new();
    public string PicturePath { get; set; } = string.Empty;
    public long BlogCategoryId { get; set; }
    public List<long> BlogTagIds { get; set; } = new();
}

public sealed class AddBlogCommandRes : ServiceResContextBase
{
}

public sealed class BlogLocalizedProperty
{
    public string Language { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}