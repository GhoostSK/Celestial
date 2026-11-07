using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Newsletter;

public sealed class SubscribeNewsletterCommandReq : BaseEntity, IRequest<SubscribeNewsletterCommandRes>
{
    public string Email { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}

public sealed class SubscribeNewsletterCommandRes : ServiceResContextBase
{
    public long Id { get; set; }

    /// <summary>اگر این ایمیل قبلاً عضو فعال بوده باشد، مقدار true برمی‌گردد.</summary>
    public bool AlreadySubscribed { get; set; }
}
