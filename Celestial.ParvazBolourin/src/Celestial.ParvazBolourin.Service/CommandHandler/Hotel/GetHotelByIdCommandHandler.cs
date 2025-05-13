using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Model.ShirdalBlog;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.ShirdalBlog;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;
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
    private readonly IImageService _imageService;

    public GetHotelByIdCommandHandler(IHotelService hotel, ICommentsService hotelComments,
        ILocalizedPropertyService localizedProperty, ILanguageService languageService, ICountryService countryService, IImageService imageService)
    {
        _hotelService = hotel;
        _localizedPropertyService = localizedProperty;
        _languageService = languageService;
        _countryService = countryService;
        _imageService = imageService;
        _hotelCommentsService = hotelComments;
    }

    protected override async Task<GetHotelByIdCommandRes> HandleCore(GetHotelByIdCommandReq request, CancellationToken cancellationToken)
    {
        var hotel = new Hotels();
        if (request.HotelId != 0)
        {
            var languages = await _languageService.GetLanguages(cancellationToken);
            if (!languages.Any(p => p.Id == request.LanguageId))
                return new GetHotelByIdCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };
            hotel = await _hotelService.GetHotelById(request.HotelId, cancellationToken: cancellationToken);
            if (hotel == null)
                return new GetHotelByIdCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("406", "Hotel Not Found")
                };

        }
        else if (!string.IsNullOrEmpty(request.Slug))
        {
            var localizedProperty = await _localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug && p.LocalKeyGroup == "Hotel", cancellationToken: cancellationToken);

            if (localizedProperty == null)
                return new GetHotelByIdCommandRes
                {
                    Error = new ValidationError("404", "Hotel not found with slug")
                };

            hotel = await _hotelService.GetHotelById(localizedProperty.EntityId, cancellationToken);
            request.LanguageId = localizedProperty.LanguageId;
            request.HotelId = localizedProperty.EntityId;
        }


        var response = hotel.Adapt<GetHotelByIdCommandRes>();
        response.HotelCountry = _countryService.FindByIdAsync(cancellationToken, hotel!.CountryId).Result.Name;

        response.HotelPictures = await _imageService.GetImagesByEntityId(hotel.Id, "Hotel", cancellationToken);

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
        response.Slug= localizedProperties.FirstOrDefault(p => p.LocalKey == "Slug")!.LocalValue;
        response.MetaKeywords= localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaKeywords")!.LocalValue;
        response.MetaDescription= localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaDescription")!.LocalValue;
        response.MetaTitle= localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaTitle")!.LocalValue;
        return response;
    }
}