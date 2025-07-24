using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Visa;
using Celestial.Agency.Service.Command.Visa;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Visa;

public sealed class GetVisaListCommandHandler(
    IVisaService visaService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
    : BaseRequestHandler<GetVisaListCommandReq, GetVisaListCommandRes>
{
    protected override async Task<GetVisaListCommandRes> HandleCore(GetVisaListCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (languages.Any(p => p.LanguageCulture.Split('-')[0] == request.Language))
            return new GetVisaListCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var response = new GetVisaListCommandRes();
        var visas = await visaService.GetVisaList(request.PageNumber, request.PageSize, cancellationToken);

        foreach (var visa in visas)
        {
            var visaItem = visa.Adapt<GetVisaListItemRes>();

            visaItem.PicturePath = visa.PicturePath;

            var localizedProperties = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = visa.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Visa
                },
                cancellationToken
            );

            visaItem.Text = localizedProperties.FirstOrDefault(p => p.LocalKey == "Text")?.LocalValue ?? string.Empty;
            visaItem.Name = localizedProperties.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty;
            visaItem.Language = request.Language;

            response.Items.Add(visaItem);
        }

        return response;
    }
}