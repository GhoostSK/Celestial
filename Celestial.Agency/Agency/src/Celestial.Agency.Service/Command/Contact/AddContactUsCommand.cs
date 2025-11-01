using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Contact;

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