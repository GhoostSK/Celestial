using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;

public sealed class AddBlogTagCommandReq : BaseEntity, IRequest<AddBlogTagCommandRes>
{
    public List<LocalizedTagNameDto> LocalizedNames { get; set; } = new();
}

public sealed class AddBlogTagCommandRes : ServiceResContextBase
{
}

public sealed class LocalizedTagNameDto
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class BlogTagListItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; }
}