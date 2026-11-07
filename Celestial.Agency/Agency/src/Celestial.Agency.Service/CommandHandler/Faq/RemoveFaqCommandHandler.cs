using Celestial.Agency.Core.DAL.Repositories.Faq;
using Celestial.Agency.Service.Command.Faq;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Faq;

public sealed class RemoveFaqCommandHandler(IFaqService faqService)
    : BaseRequestHandler<RemoveFaqCommandReq, RemoveFaqCommandRes>
{
    protected override async Task<RemoveFaqCommandRes> HandleCore(RemoveFaqCommandReq request,
        CancellationToken cancellationToken)
    {
        var faq = await faqService.GetFaqById(request.Id, cancellationToken);

        if (faq == null)
            return Failure("404", "Faq not found");

        faq.IsDeleted = true;

        faqService.Update(faq);
        await faqService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new RemoveFaqCommandRes();
    }
}
