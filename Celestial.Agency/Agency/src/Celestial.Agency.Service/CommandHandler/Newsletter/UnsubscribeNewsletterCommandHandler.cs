using Celestial.Agency.Core.DAL.Repositories.Newsletter;
using Celestial.Agency.Service.Command.Newsletter;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Newsletter;

public sealed class UnsubscribeNewsletterCommandHandler(INewsletterSubscriptionService subscriptionService)
    : BaseRequestHandler<UnsubscribeNewsletterCommandReq, UnsubscribeNewsletterCommandRes>
{
    protected override async Task<UnsubscribeNewsletterCommandRes> HandleCore(
        UnsubscribeNewsletterCommandReq request, CancellationToken cancellationToken)
    {
        var email = request.Email?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email))
            return Failure("400", "Email is required.");

        var subscription = await subscriptionService.GetByEmail(email, cancellationToken);

        if (subscription == null)
            return Failure("404", "Subscription not found");

        subscription.IsActive = false;
        subscription.UnsubscribedAt = DateTime.UtcNow;

        subscriptionService.Update(subscription);
        await subscriptionService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UnsubscribeNewsletterCommandRes();
    }
}
