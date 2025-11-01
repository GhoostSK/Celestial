using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;

public sealed class DeleteBlogTagCommandReq : BaseEntity, IRequest<DeleteBlogTagCommandRes>
{
    public long Id { get; set; }
}

public sealed class DeleteBlogTagCommandRes : ServiceResContextBase
{
}