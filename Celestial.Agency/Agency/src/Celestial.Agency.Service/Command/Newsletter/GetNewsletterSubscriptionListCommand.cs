using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Newsletter;

public sealed class GetNewsletterSubscriptionListCommandReq : BaseEntity,
    IRequest<GetNewsletterSubscriptionListCommandRes>
{
    public bool OnlyActive { get; set; } = true;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public sealed class GetNewsletterSubscriptionListCommandRes : ServiceResContextBase
{
    public List<NewsletterSubscriptionItemRes> Items { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();
}

public sealed class NewsletterSubscriptionItemRes
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UnsubscribedAt { get; set; }
}
