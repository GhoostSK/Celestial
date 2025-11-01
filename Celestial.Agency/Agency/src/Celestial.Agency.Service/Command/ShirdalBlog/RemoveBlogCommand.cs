using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class RemoveBlogCommandReq : BaseEntity, IRequest<RemoveBlogCommandRes>
{
    public int Id { get; set; }
}

public sealed class RemoveBlogCommandRes : ServiceResContextBase
{
}