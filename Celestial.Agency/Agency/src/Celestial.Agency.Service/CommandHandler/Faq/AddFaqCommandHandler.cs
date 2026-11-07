using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Faq;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Service.Command.Faq;
using Celestial.Agency.Service.Command.localization;
using Celestial.SharedKernel.Mediator;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Faq;

public sealed class AddFaqCommandHandler(
    IFaqService faqService,
    ILocalizedPropertyService localizedPropertyService,
    IMediator mediator)
    : BaseRequestHandler<AddFaqCommandReq, AddFaqCommandRes>
{
    protected override async Task<AddFaqCommandRes> HandleCore(AddFaqCommandReq request,
        CancellationToken cancellationToken)
    {
        if (!request.LocalizedProperty.Any())
            return Failure("400", "At least one localized question is required.");

        var languages = request.LocalizedProperty
            .Select(p => p.Language = p.Language?.ToLowerInvariant() ?? string.Empty)
            .ToList();

        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = languages }, cancellationToken);

        if (validateLanguage.HasError)
            return new AddFaqCommandRes { Error = validateLanguage.Error };

        var faq = new Core.DAL.Model.Faq.Faq
        {
            DisplayOrder = request.DisplayOrder,
            Published = request.Published
        };

        await faqService.AddAsync(faq, cancellationToken);
        await faqService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var localizedProperties = request.LocalizedProperty
            .SelectMany(item => new[]
            {
                new LocalizedProperty
                {
                    EntityId = faq.Id,
                    Language = item.Language,
                    LocalKey = nameof(item.Question),
                    LocalValue = item.Question,
                    LocalKeyGroup = LocalKeyGroupType.Faq
                },
                new LocalizedProperty
                {
                    EntityId = faq.Id,
                    Language = item.Language,
                    LocalKey = nameof(item.Answer),
                    LocalValue = item.Answer,
                    LocalKeyGroup = LocalKeyGroupType.Faq
                }
            })
            .ToList();

        await localizedPropertyService.AddRangeAsync(localizedProperties, cancellationToken);
        await localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddFaqCommandRes { Id = faq.Id };
    }
}
