using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Contact;

public sealed class AddContactUsCommandReq : BaseEntity, IRequest<AddContactUsCommandRes>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
}

public sealed class AddContactUsCommandRes : ServiceResContextBase
{
}


