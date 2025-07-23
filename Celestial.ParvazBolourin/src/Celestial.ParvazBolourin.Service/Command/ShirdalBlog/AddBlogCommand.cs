using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog;

public sealed class AddBlogCommandReq : BaseEntity, IRequest<AddBlogCommandRes>
{
    public List<BlogLocalizedProperty> BlogLocalizedProperties { get; set; } = new();
    public string PicturePath { get; set; } = string.Empty;
    public List<long> BlogCategoryIds { get; set; } = new();
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

