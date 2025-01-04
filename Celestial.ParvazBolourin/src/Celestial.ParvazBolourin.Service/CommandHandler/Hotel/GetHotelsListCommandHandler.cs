using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
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

    public GetHotelsListCommandHandler(IHotelService hotel, ILocalizedPropertyService localizedProperty, ILanguageService languageService)
    {
        _hotelRepository = hotel;
        _localizedProperty = localizedProperty;
        _languageService = languageService;
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

        foreach (var item in hotels)
        {
            var picturePath = item.HotelPictures
                .FirstOrDefault(p => p.DisplayOrder == 1)?.VirtualPath ?? "Not Found";

            var localize = await _localizedProperty.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "Hotel"
                },
                cancellationToken
            );

            var shortText = localize.SingleOrDefault(p => p.LocalKey == "ShortText")?.LocalValue;

            response.Items.Add(new GetAllHotelsServiceRes
            {
                HotelId = item.Id,
                ImagePath = picturePath,
                BookingUrl = item.BookingUrl,
                HotelCountry = item.HotelCountry,
                HotelReception = item.HotelReception,
                HotelScore = item.HotelScore,
                Name = item.Name,

                ShortText = shortText
            });
        }

        return response;
    }
}