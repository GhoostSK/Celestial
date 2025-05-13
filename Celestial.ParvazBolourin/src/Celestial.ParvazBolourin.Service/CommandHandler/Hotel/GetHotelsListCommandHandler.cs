using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Hotel;

public sealed class GetHotelsListCommandHandler : BaseRequestHandler<GetHotelsListCommandReq, GetHotelsListCommandRes>
{
    private readonly IHotelService _hotelRepository;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedProperty;
    private readonly ICountryService _countryService;
    private readonly IImageService _imageService;

    public GetHotelsListCommandHandler(IHotelService hotel, ILocalizedPropertyService localizedProperty,
        ILanguageService languageService, ICountryService countryService, IImageService imageService)
    {
        _hotelRepository = hotel;
        _localizedProperty = localizedProperty;
        _languageService = languageService;
        _countryService = countryService;
        _imageService = imageService;
    }

    protected override async Task<GetHotelsListCommandRes> HandleCore(GetHotelsListCommandReq request,
    CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetHotelsListCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var response = new GetHotelsListCommandRes();
        var hotels = await _hotelRepository.GetHotelsList(request.Adapt<GetHotelsListReq>(), cancellationToken);

        if (!hotels.Any())
            return response;

        response.MinPrice = hotels.Min(h => h.Price);
        response.MaxPrice = hotels.Max(h => h.Price);

        foreach (var item in hotels)
        {
            var picture = await _imageService.FirstOrDefaultAsync(
                p => p.KeyGroup == "Hotel" && p.EntityId == item.Id,
                cancellationToken: cancellationToken);

            var localize = await _localizedProperty.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "Hotel"
                },
                cancellationToken
            );

            var slug = localize.SingleOrDefault(p => p.LocalKey == "Slug")?.LocalValue;
            var shortText = localize.SingleOrDefault(p => p.LocalKey == "ShortText")?.LocalValue;
            var country = await _countryService.FindByIdAsync(cancellationToken, item.CountryId);
            var countryName = country?.Name ?? "";

            response.Items.Add(new GetAllHotelsServiceRes
            {
                HotelId = item.Id,
                ImagePath = picture?.PicturePath!,
                BookingUrl = item.BookingUrl,
                HotelCountry = countryName,
                HotelReception = item.HotelReception,
                HotelScore = item.HotelScore,
                Name = item.Name,
                ShortText = shortText,
                Slug = slug
            });
        }

        return response;
    }

}