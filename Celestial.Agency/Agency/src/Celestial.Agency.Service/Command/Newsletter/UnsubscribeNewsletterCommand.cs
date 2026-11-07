using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Newsletter;

public sealed class UnsubscribeNewsletterCommandReq : BaseEntity, IRequest<UnsubscribeNewsletterCommandRes>
{
    public string Email { get; set; } = string.Empty;
}

public sealed class UnsubscribeNewsletterCommandRes : ServiceResContextBase
{
}
