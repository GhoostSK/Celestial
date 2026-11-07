using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Faq;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Service.Command.Faq;
using Celestial.Agency.Service.Command.localization;
using Celestial.SharedKernel.Mediator;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Faq;

public sealed class UpdateFaqCommandHandler(
    IFaqService faqService,
    ILocalizedPropertyService localizedPropertyService,
    IMediator mediator)
    : BaseRequestHandler<UpdateFaqCommandReq, UpdateFaqCommandRes>
{
    protected override async Task<UpdateFaqCommandRes> HandleCore(UpdateFaqCommandReq request,
        CancellationToken cancellationToken)
    {
        var faq = await faqService.GetFaqById(request.Id, cancellationToken);

        if (faq == null)
            return Failure("404", "Faq not found");

        var languages = request.LocalizedProperty
            .Select(p => p.Language = p.Language?.ToLowerInvariant() ?? string.Empty)
            .ToList();

        if (languages.Any())
        {
            var validateLanguage =
                await mediator.Send(new ValidateLanguageCommandReq { Language = languages }, cancellationToken);

            if (validateLanguage.HasError)
                return new UpdateFaqCommandRes { Error = validateLanguage.Error };
        }

        faq.DisplayOrder = request.DisplayOrder;
        faq.Published = request.Published;

        faqService.Update(faq);

        foreach (var item in request.LocalizedProperty)
        {
            await UpsertLocalizedValueAsync(faq.Id, item.Language, nameof(item.Question), item.Question,
                cancellationToken);
            await UpsertLocalizedValueAsync(faq.Id, item.Language, nameof(item.Answer), item.Answer,
                cancellationToken);
        }

        await faqService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateFaqCommandRes();
    }

    private async Task UpsertLocalizedValueAsync(long faqId, string language, string key, string value,
        CancellationToken cancellationToken)
    {
        var existing = await localizedPropertyService.FirstOrDefaultAsync(
            p => p.EntityId == faqId
                 && p.LocalKeyGroup == LocalKeyGroupType.Faq
                 && p.Language == language
                 && p.LocalKey == key,
            false, cancellationToken);

        if (existing == null)
        {
            await localizedPropertyService.AddAsync(new LocalizedProperty
            {
                EntityId = faqId,
                Language = language,
                LocalKey = key,
                LocalValue = value,
                LocalKeyGroup = LocalKeyGroupType.Faq
            }, cancellationToken);

            return;
        }

        existing.LocalValue = value;
        localizedPropertyService.Update(existing);
    }
}
