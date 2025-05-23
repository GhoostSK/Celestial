using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogTag;

public sealed class GetBlogTagByIdCommandReq : BaseEntity, IRequest<GetBlogTagByIdCommandRes>
{
    public long Id { get; set; }
    public int LanguageId { get; set; }
}

public sealed class GetBlogTagByIdCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}