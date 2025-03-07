using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Hotel;

public sealed class GetHotelByIdCommandHandler : BaseRequestHandler<GetHotelByIdCommandReq, GetHotelByIdCommandRes>
{
    private readonly ICommentsService _hotelCommentsService;
    private readonly IHotelService _hotelService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ICountryService _countryService;

    public GetHotelByIdCommandHandler(IHotelService hotel, ICommentsService hotelComments,
        ILocalizedPropertyService localizedProperty, ILanguageService languageService, ICountryService countryService)
    {
        _hotelService = hotel;
        _localizedPropertyService = localizedProperty;
        _languageService = languageService;
        _countryService = countryService;
        _hotelCommentsService = hotelComments;
    }

    protected override async Task<GetHotelByIdCommandRes> HandleCore(GetHotelByIdCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetHotelByIdCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var hotel = await _hotelService.FindByIdAsync(cancellationToken, request.HotelId);
        if (hotel == null)
            return new GetHotelByIdCommandRes
            {
                Error = ValidationError.UnhandledValidationError("406", "Hotel Not Found")
            };

        var response = hotel.Adapt<GetHotelByIdCommandRes>();
        response.HotelCountry = _countryService.FindByIdAsync(cancellationToken, hotel.CountryId).Result.Name;
        response.HotelPictures = hotel.HotelPictures
            .Select(p => new HotelPicture
            {
                DisplayOrder = p.DisplayOrder,
                VirtualPath = p.VirtualPath
            })
            .ToList();

        response.HotelComments = await _hotelCommentsService.GetComments(request.HotelId, cancellationToken);

        var localizedProperties = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = request.HotelId,
                LanguageId = request.LanguageId,
                LocalKeyGroup = "Hotel"
            },
            cancellationToken
        );
        if (localizedProperties.Count == 0)
            return new GetHotelByIdCommandRes
            {
                Error = new ValidationError("500", "Language not found")
            };
        response.Address = localizedProperties.FirstOrDefault(p => p.LocalKey == "Address")!.LocalValue!;
        response.ShortText = localizedProperties.FirstOrDefault(p => p.LocalKey == "ShortText")!.LocalValue;
        response.MainText = localizedProperties.FirstOrDefault(p => p.LocalKey == "MainText")!.LocalValue;

        return response;
    }
}