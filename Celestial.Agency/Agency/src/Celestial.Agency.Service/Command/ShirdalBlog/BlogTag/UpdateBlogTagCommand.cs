using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;

public sealed class UpdateBlogTagCommandReq : BaseEntity, IRequest<UpdateBlogTagCommandRes>
{
    public long Id { get; set; }
    public List<LocalizedTagNameDto> LocalizedNames { get; set; } = new();
}

public sealed class UpdateBlogTagCommandRes : ServiceResContextBase
{
}