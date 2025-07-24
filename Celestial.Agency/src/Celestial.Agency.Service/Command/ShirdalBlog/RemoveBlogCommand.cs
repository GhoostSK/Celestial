using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public sealed class RemoveBlogCommandReq : BaseEntity, IRequest<RemoveBlogCommandRes>
{
    public int Id { get; set; }
}

public sealed class RemoveBlogCommandRes : ServiceResContextBase
{
}