using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Visa;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Celestial.ParvazBolourin.Service.Command.Visa;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Visa;

public sealed class AddVisaCommandHandler : BaseRequestHandler<AddVisaCommandReq, AddVisaCommandRes>
{
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly IVisaService _visaService;

    public AddVisaCommandHandler(IVisaService visaService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _visaService = visaService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    protected override async Task<AddVisaCommandRes> HandleCore(AddVisaCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        foreach (var item in request.VisaLocalizedProperty)
            if (!languages.Any(lang => lang.Id == item.LanguageId))
                return new AddVisaCommandRes
                {
                    Error = new ValidationError("LA400", "Invalid Language")
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
        var visaId = await _visaService.AddVisa(visa, cancellationToken);

        var localizedProperties = new List<LocalizedProperty>();

        foreach (var item in request.VisaLocalizedProperty)
        {
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = visaId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.Text),
                LocalValue = item.Text,
                LocalKeyGroup = "Visa"
            });

            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = visaId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.Name),
                LocalValue = item.Name,
                LocalKeyGroup = "Visa"
            });

            foreach (var embassy in item.Embassy)
            foreach (var kvp in embassy)
                localizedProperties.Add(new LocalizedProperty
                {
                    EntityId = visaId,
                    LanguageId = item.LanguageId,
                    LocalKey = "Embassy_" + kvp.Key,
                    LocalValue = kvp.Value,
                    LocalKeyGroup = "Visa"
                });
        }

        await _localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);

        return new AddVisaCommandRes();
    }
}