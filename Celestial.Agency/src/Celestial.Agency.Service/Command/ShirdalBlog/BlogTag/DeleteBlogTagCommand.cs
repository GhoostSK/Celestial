using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;

public sealed class DeleteBlogTagCommandReq : BaseEntity, IRequest<DeleteBlogTagCommandRes>
{
    public long Id { get; set; }
}

public sealed class DeleteBlogTagCommandRes : ServiceResContextBase
{
}