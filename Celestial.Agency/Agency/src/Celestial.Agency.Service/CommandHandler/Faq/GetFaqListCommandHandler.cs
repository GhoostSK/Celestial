using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Faq;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Service.Command.Faq;
using Celestial.Agency.Service.Command.localization;
using Celestial.SharedKernel.Mediator;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Faq;

public sealed class GetFaqListCommandHandler(
    IFaqService faqService,
    ILocalizedPropertyService localizedPropertyService,
    IMediator mediator)
    : BaseRequestHandler<GetFaqListCommandReq, GetFaqListCommandRes>
{
    protected override async Task<GetFaqListCommandRes> HandleCore(GetFaqListCommandReq request,
        CancellationToken cancellationToken)
    {
        var validateLanguage = await mediator.Send(
            new ValidateLanguageCommandReq { Language = new List<string> { request.Language } }, cancellationToken);

        if (validateLanguage.HasError)
            return new GetFaqListCommandRes { Error = validateLanguage.Error };

        var faqs = await faqService.GetFaqList(request.OnlyPublished, cancellationToken);
        var response = new GetFaqListCommandRes();

        foreach (var faq in faqs)
        {
            var localizedData = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = faq.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Faq
                }, cancellationToken);

            var question = localizedData
                .FirstOrDefault(p => p.LocalKey == nameof(FaqItemRes.Question))?.LocalValue;

            // پرسشی که برای این زبان ترجمه ندارد نمایش داده نمی‌شود
            if (string.IsNullOrWhiteSpace(question))
                continue;

            response.Items.Add(new FaqItemRes
            {
                Id = faq.Id,
                Question = question,
                Answer = localizedData.FirstOrDefault(p => p.LocalKey == nameof(FaqItemRes.Answer))?.LocalValue
                         ?? string.Empty,
                DisplayOrder = faq.DisplayOrder,
                Published = faq.Published
            });
        }

        return response;
    }
}
