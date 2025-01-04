using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Contact;


public sealed class GetContactUsListCommandReq : BaseEntity, IRequest<GetContactUsListCommandRes>
{
}

public sealed class GetContactUsListCommandRes : ServiceResContextBase
{
    public List<ContactUsDto> ContactUsList { get; set; } = new();
}

public sealed class ContactUsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
