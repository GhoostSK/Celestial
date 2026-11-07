using System.Net.Mail;
using Celestial.Agency.Core.DAL.Model.Newsletter;
using Celestial.Agency.Core.DAL.Repositories.Newsletter;
using Celestial.Agency.Service.Command.Newsletter;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Newsletter;

public sealed class SubscribeNewsletterCommandHandler(INewsletterSubscriptionService subscriptionService)
    : BaseRequestHandler<SubscribeNewsletterCommandReq, SubscribeNewsletterCommandRes>
{
    protected override async Task<SubscribeNewsletterCommandRes> HandleCore(SubscribeNewsletterCommandReq request,
        CancellationToken cancellationToken)
    {
        var email = request.Email?.Trim().ToLowerInvariant() ?? string.Empty;

        if (!MailAddress.TryCreate(email, out _))
            return Failure("400", "The email address is not valid.");

        var existing = await subscriptionService.GetByEmail(email, cancellationToken);

        if (existing != null)
        {
            if (existing.IsActive)
                return new SubscribeNewsletterCommandRes
                {
                    Id = existing.Id,
                    AlreadySubscribed = true
                };

            // عضویت قبلی لغو شده بود؛ دوباره فعال می‌شود
            existing.IsActive = true;
            existing.UnsubscribedAt = null;
            existing.Language = request.Language;

            subscriptionService.Update(existing);
            await subscriptionService.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new SubscribeNewsletterCommandRes { Id = existing.Id };
        }

        var subscription = new NewsletterSubscription
        {
            Email = email,
            Language = request.Language
        };

        await subscriptionService.AddAsync(subscription, cancellationToken);
        await subscriptionService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new SubscribeNewsletterCommandRes { Id = subscription.Id };
    }
}
