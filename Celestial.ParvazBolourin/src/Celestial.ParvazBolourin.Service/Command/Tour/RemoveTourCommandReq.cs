using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour;

public sealed class RemoveTourCommandReq : BaseEntity, IRequest<RemoveTourCommandRes>
{
    public int Id { get; set; }
}

public sealed class RemoveTourCommandRes : ServiceResContextBase
{
}