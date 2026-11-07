using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Visa;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Visa;
using Celestial.Agency.Service.Command.Visa;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;
using Mapster;

namespace Celestial.Agency.Service.CommandHandler.Visa;

public sealed class GetVisaByIdCommandHandler(
    IVisaService visaService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService)
    : BaseRequestHandler<GetVisaByIdCommandReq, GetVisaCommandRes>
{
    private readonly ILanguageService _languageService = languageService;

    protected override async Task<GetVisaCommandRes> HandleCore(GetVisaByIdCommandReq request,
        CancellationToken cancellationToken)
    {
        var visa = new Core.DAL.Model.Visa.Visa();

        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(p =>
            p.LocalKey == "Slug" &&
            p.LocalValue == request.Slug &&
            p.LocalKeyGroup == LocalKeyGroupType.Visa, cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetVisaCommandRes
            {
                Error = new ValidationError("VISA404", "Visa not found by slug")
            };

        visa = await visaService.GetVisaById(localizedProperty.EntityId, cancellationToken);

        if (visa == null)
            return new GetVisaCommandRes
            {
                Error = new ValidationError("VISA404", "Visa not found")
            };

        var response = visa.Adapt<GetVisaCommandRes>();
        response.Language = localizedProperty.Language;
        response.Slug = localizedProperty.LocalValue;

        var localizedProperties = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = visa.Id,
                Language = localizedProperty.Language,
                LocalKeyGroup = LocalKeyGroupType.Visa
            },
            cancellationToken
        );

        response.Text = localizedProperties.SingleOrDefault(p => p.LocalKey == "Text")?.LocalValue ?? string.Empty;
        response.Name = localizedProperties.SingleOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty;

        var embassy = localizedProperties
            .Where(p => p.LocalKey == "Embassy_Address" ||
                        (p.LocalKey == "Embassy_PhoneNumber" && p.Language == localizedProperty.Language))
            .ToList();

        var address = embassy.Where(e => e.LocalKey == "Embassy_Address").ToList();
        var phoneNumber = embassy.Where(e => e.LocalKey == "Embassy_PhoneNumber").ToList();

        for (var i = 0; i < address.Count(); i++)
            response.Embassy.Add(new Dictionary<string, string>
            {
                { "Address : ", address[i].LocalValue },
                { "phoneNumber : ", phoneNumber.ElementAtOrDefault(i)?.LocalValue ?? string.Empty }
            });

        return response;
    }
}