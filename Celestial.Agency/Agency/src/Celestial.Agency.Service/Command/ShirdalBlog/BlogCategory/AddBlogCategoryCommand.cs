using System.ComponentModel.DataAnnotations;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;

public sealed class AddBlogCategoryCommandReq : BaseEntity, IRequest<AddBlogCategoryCommandRes>
{
    public List<LocalizedNameDto> LocalizeProperty { get; set; } = new();
}

public sealed class AddBlogCategoryCommandRes : ServiceResContextBase
{
}

public sealed class LocalizedNameDto
{
    [Required] public string Language { get; set; } = string.Empty;

    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Slug { get; set; } = string.Empty;
}

public sealed class BlogCategoryItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}