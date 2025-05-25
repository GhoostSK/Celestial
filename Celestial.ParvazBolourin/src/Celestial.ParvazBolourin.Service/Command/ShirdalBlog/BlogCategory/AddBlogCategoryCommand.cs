using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;

public sealed class AddBlogCategoryCommandReq : BaseEntity, IRequest<AddBlogCategoryCommandRes>
{
    public List<LocalizedNameDto> LocalizedNames { get; set; } = new();
}

public sealed class AddBlogCategoryCommandRes : ServiceResContextBase
{
}

public sealed class LocalizedNameDto
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogCategoryListItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; }
}