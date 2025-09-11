using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Visa;
using Celestial.Agency.Service.Command.localization;
using Celestial.Agency.Service.Command.Visa;
using MediatR;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.Agency.Service.CommandHandler.Visa;

public sealed class AddVisaCommandHandler(
    IVisaService visaService,
    ILocalizedPropertyService localizedPropertyService,
    IMediator mediator)
    : BaseRequestHandler<AddVisaCommandReq, AddVisaCommandRes>
{
    protected override async Task<AddVisaCommandRes> HandleCore(AddVisaCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string>();
        foreach (var lang in request.VisaLocalizedProperty)
        {
            lang.Language = lang.Language?.ToLowerInvariant()!;
            langs.Add(lang.Language);
        }

        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new AddVisaCommandRes
            {
                Error = validateLanguage.Error
            };
        //foreach (var localizeSlug in request.VisaLocalizedProperty)
        //{
        //    var checkSlug = await _localizedPropertyService.FirstOrDefaultAsync(p => p.LocalKeyGroup == "Visa" && p.LocalKey == "Slug" && p.LocalValue == localizeSlug.Slug && p.LanguageId == localizeSlug.LanguageId, cancellationToken: cancellationToken);
        //    if (checkSlug != null)
        //    {
        //        return new AddVisaCommandRes()
        //        {
        //            Error = ValidationError.UnhandledValidationError("S403", $"Slug Already exist({checkSlug.LanguageId})")
        //        };
        //    }
        //}
        var visa = request.Visa;
        var visaId = await visaService.AddVisa(visa, cancellationToken);

        var localizedProperties = new List<LocalizedProperty>();

        foreach (var item in request.VisaLocalizedProperty)
        {
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = visaId,
                Language = item.Language,
                LocalKey = nameof(item.Text),
                LocalValue = item.Text,
                LocalKeyGroup = LocalKeyGroupType.Visa
            });

            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = visaId,
                Language = item.Language,
                LocalKey = nameof(item.Name),
                LocalValue = item.Name,
                LocalKeyGroup = LocalKeyGroupType.Visa
            });

            foreach (var embassy in item.Embassy)
            foreach (var kvp in embassy)
                localizedProperties.Add(new LocalizedProperty
                {
                    EntityId = visaId,
                    Language = item.Language,
                    LocalKey = "Embassy_" + kvp.Key,
                    LocalValue = kvp.Value,
                    LocalKeyGroup = LocalKeyGroupType.Visa
                });
        }

        await localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);

        return new AddVisaCommandRes();
    }
}