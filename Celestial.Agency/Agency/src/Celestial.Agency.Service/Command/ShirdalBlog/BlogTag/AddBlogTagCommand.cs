using System.ComponentModel.DataAnnotations;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;

public sealed class AddBlogTagCommandReq : BaseEntity, IRequest<AddBlogTagCommandRes>
{
    public List<LocalizedTagNameDto> LocalizedNames { get; set; } = new();
}

public sealed class AddBlogTagCommandRes : ServiceResContextBase
{
}

public sealed class LocalizedTagNameDto
{
    [Required] public string Language { get; set; } = string.Empty;

    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Slug { get; set; } = string.Empty;
}

public sealed class BlogTagListItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}