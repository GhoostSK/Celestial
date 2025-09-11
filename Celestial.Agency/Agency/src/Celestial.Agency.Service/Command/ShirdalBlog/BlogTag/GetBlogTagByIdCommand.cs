using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;

public sealed class GetBlogTagBySlugCommandReq : BaseEntity, IRequest<GetBlogTagBySlugCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class GetBlogTagBySlugCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}