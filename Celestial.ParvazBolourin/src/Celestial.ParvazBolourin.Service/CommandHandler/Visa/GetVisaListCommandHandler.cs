using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Visa;
using Celestial.ParvazBolourin.Service.Command.Visa;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Visa;

public sealed class GetVisaListCommandHandler : BaseRequestHandler<GetVisaListCommandReq, GetVisaListCommandRes>
{
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly IVisaService _visaService;

    public GetVisaListCommandHandler(IVisaService visaService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _visaService = visaService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    protected override async Task<GetVisaListCommandRes> HandleCore(GetVisaListCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetVisaListCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var response = new GetVisaListCommandRes();
        var visas = await _visaService.GetVisaList(request.PageNumber, request.PageSize, cancellationToken);

        foreach (var visa in visas)
        {
            var visaItem = visa.Adapt<GetVisaListItemRes>();

            visaItem.PicturePath = visa.PicturePath;

            var localizedProperties = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = visa.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "Visa"
                },
                cancellationToken
            );

            visaItem.Text = localizedProperties.FirstOrDefault(p => p.LocalKey == "Text")?.LocalValue ?? string.Empty;
            visaItem.Name = localizedProperties.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty;
            visaItem.LanguageId = request.LanguageId;

            response.Items.Add(visaItem);
        }
        return response;
    }
}