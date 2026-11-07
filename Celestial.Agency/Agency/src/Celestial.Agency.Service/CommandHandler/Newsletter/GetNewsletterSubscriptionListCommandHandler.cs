using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Newsletter;
using Celestial.Agency.Service.Command.Newsletter;
using Celestial.SharedKernel.Mediator;
using Mapster;

namespace Celestial.Agency.Service.CommandHandler.Newsletter;

public sealed class GetNewsletterSubscriptionListCommandHandler(INewsletterSubscriptionService subscriptionService)
    : BaseRequestHandler<GetNewsletterSubscriptionListCommandReq, GetNewsletterSubscriptionListCommandRes>
{
    protected override async Task<GetNewsletterSubscriptionListCommandRes> HandleCore(
        GetNewsletterSubscriptionListCommandReq request, CancellationToken cancellationToken)
    {
        var (items, totalPages, totalRecord) = await subscriptionService.GetSubscriptionList(
            request.OnlyActive, request.PageNumber, request.PageSize, cancellationToken);

        return new GetNewsletterSubscriptionListCommandRes
        {
            Items = items.Adapt<List<NewsletterSubscriptionItemRes>>(),
            Pagination = new PaginationMeta
            {
                TotalRecords = totalRecord,
                CurrentPage = request.PageNumber,
                TotalPages = totalPages,
                NextPage = request.PageNumber < totalPages ? request.PageNumber + 1 : null,
                PrevPage = request.PageNumber > 1 ? request.PageNumber - 1 : null
            }
        };
    }
}
