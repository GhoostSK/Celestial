using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class GetLatestBlogListCommandReq : BaseEntity, IRequest<GetLatestBlogListCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class GetLatestBlogListCommandRes : ServiceResContextBase
{
    public List<LatestBlogItem> Items { get; set; } = new();
}