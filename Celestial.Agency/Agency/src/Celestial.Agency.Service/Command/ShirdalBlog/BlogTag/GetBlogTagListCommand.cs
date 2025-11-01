using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;

public sealed class GetBlogTagListCommandReq : BaseEntity, IRequest<GetBlogTagListCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class GetBlogTagListCommandRes : ServiceResContextBase
{
    public List<BlogTagListItemDto> Items { get; set; } = new();
    public int ReturnedCount { get; set; }
}