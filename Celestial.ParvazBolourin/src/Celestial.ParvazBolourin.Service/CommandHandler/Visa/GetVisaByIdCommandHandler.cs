using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Visa;
using Celestial.ParvazBolourin.Service.Command.Visa;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Visa;

public sealed class GetVisaByIdCommandHandler : BaseRequestHandler<GetVisaByIdCommandReq, GetVisaByIdCommandRes>
{
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly IVisaService _visaService;

    public GetVisaByIdCommandHandler(IVisaService visaService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _visaService = visaService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    protected override async Task<GetVisaByIdCommandRes> HandleCore(GetVisaByIdCommandReq request,
        CancellationToken cancellationToken)
    {


        var visa = new Visas();

        if (!string.IsNullOrWhiteSpace(request.Slug))
        {
            var localizedProperty = await _localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" &&
                p.LocalValue == request.Slug &&
                p.LocalKeyGroup == "Visa", cancellationToken: cancellationToken);

            if (localizedProperty == null)
            {
                return new GetVisaByIdCommandRes
                {
                    Error = new ValidationError("VISA404", "Visa not found by slug")
                };
            }

            visa = await _visaService.GetVisaById(localizedProperty.EntityId, cancellationToken);
            request.LanguageId = localizedProperty.LanguageId;
        }
        else
        {
            var languages = await _languageService.GetLanguages(cancellationToken);
            if (!languages.Any(p => p.Id == request.LanguageId))
                return new GetVisaByIdCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };
            visa = await _visaService.GetVisaById(request.Id, cancellationToken);
        }

        if (visa == null)
            return new GetVisaByIdCommandRes
            {
                Error = new ValidationError("VISA404", "Visa not found")
            };

        var response = visa.Adapt<GetVisaByIdCommandRes>();
        response.LanguageId = request.LanguageId;

        var localizedProperties = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = visa.Id,
                LanguageId = request.LanguageId,
                LocalKeyGroup = "Visa"
            },
            cancellationToken
        );

        response.Text = localizedProperties.SingleOrDefault(p => p.LocalKey == "Text")?.LocalValue ?? string.Empty;
        response.Name = localizedProperties.SingleOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty;

        var embassy = localizedProperties
            .Where(p => p.LocalKey == "Embassy_Address" ||
                        (p.LocalKey == "Embassy_PhoneNumber" && p.LanguageId == request.LanguageId))
            .ToList();

        var address = embassy.Where(e => e.LocalKey == "Embassy_Address").ToList();
        var phoneNumber = embassy.Where(e => e.LocalKey == "Embassy_PhoneNumber").ToList();

        for (var i = 0; i < address.Count(); i++)
        {
            response.Embassy.Add(new Dictionary<string, string>
        {
            { "Address : ", address[i].LocalValue },
            { "phoneNumber : ", phoneNumber.ElementAtOrDefault(i)?.LocalValue ?? string.Empty }
        });
        }

        return response;
    }

}