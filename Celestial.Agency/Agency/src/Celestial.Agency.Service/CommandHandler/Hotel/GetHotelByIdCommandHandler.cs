using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Hotel;
using Celestial.Agency.Service.Command.Hotel;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;
using Mapster;

namespace Celestial.Agency.Service.CommandHandler.Hotel;

public sealed class GetHotelByIdCommandHandler(
    IHotelService hotelService,
    ICommentsService hotelComments,
    ILocalizedPropertyService localizedPropertyService,
    ICountryService countryService,
    IImageService imageService)
    : BaseRequestHandler<GetHotelCommandReq, GetHotelByIdCommandRes>
{
    protected override async Task<GetHotelByIdCommandRes> HandleCore(GetHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" && p.LocalValue == request.Slug && p.LocalKeyGroup == LocalKeyGroupType.Hotel,
            cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetHotelByIdCommandRes
            {
                Error = new ValidationError("404", "Hotel not found with slug")
            };

        var hotel = await hotelService.GetHotelById(localizedProperty.EntityId, cancellationToken);

        var response = hotel.Adapt<GetHotelByIdCommandRes>();
        response.HotelCountry =
            (await countryService.FindByIdAsync(cancellationToken, hotel!.CountryId))?.Name ?? string.Empty;

        response.HotelPictures = await imageService.GetImagesByEntityId(hotel.Id, "Hotel", cancellationToken);

        response.HotelComments = await hotelComments.GetComments(hotel.Id, cancellationToken);

        var localizedProperties = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = hotel.Id,
                Language = localizedProperty.Language,
                LocalKeyGroup = LocalKeyGroupType.Hotel
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
        response.Slug = localizedProperties.FirstOrDefault(p => p.LocalKey == "Slug")!.LocalValue;
        response.MetaKeywords = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaKeywords")!.LocalValue;
        response.MetaDescription = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaDescription")!.LocalValue;
        response.MetaTitle = localizedProperties.FirstOrDefault(p => p.LocalKey == "MetaTitle")!.LocalValue;
        return response;
    }
}