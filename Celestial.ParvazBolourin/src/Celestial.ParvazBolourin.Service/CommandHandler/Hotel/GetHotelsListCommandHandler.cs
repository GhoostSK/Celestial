using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel.Model;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Celestial.ParvazBolourin.Service.Command.ValidateLanguage;
using Celestial.Service.Command.Hotel;
using Mapster;
using MediatR;
using MofidApp.SharedKernel.Common.Model;

namespace Celestial.Service.CommandHandler.Hotel;

public sealed class GetHotelsListCommandHandler(
    IHotelService hotel,
    ILocalizedPropertyService localizedProperty,
    ICountryService countryService,
    IImageService imageService,
    IMediator mediator)
    : BaseRequestHandler<GetHotelsListCommandReq, GetHotelsListCommandRes>
{
    protected override async Task<GetHotelsListCommandRes> HandleCore(GetHotelsListCommandReq request,
    CancellationToken cancellationToken)
    {
        var langs = new List<string> { request.Language };
        var validateLanguage = await mediator.Send(new ValidateLanguageCommandReq() { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
        {
            return new()
            {
                Error = validateLanguage.Error
            };
        }

        var response = new GetHotelsListCommandRes();
        var hotels = await hotel.GetHotelsList(request.Adapt<GetHotelsListReq>(), cancellationToken);

        if (!hotels.Items.Any())
            return response;

        response.MinPrice = hotels.Items.Min(h => h.Price);
        response.MaxPrice = hotels.Items.Max(h => h.Price);

        foreach (var item in hotels.Items)
        {
            var picture = await imageService.FirstOrDefaultAsync(
                p => p.KeyGroup == "Hotel" && p.EntityId == item.Id,
                cancellationToken: cancellationToken);

            var localize = await localizedProperty.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Hotel
                },
                cancellationToken
            );

            var slug = localize.SingleOrDefault(p => p.LocalKey == "Slug")?.LocalValue;
            var shortText = localize.SingleOrDefault(p => p.LocalKey == "ShortText")?.LocalValue;
            var country = await countryService.FindByIdAsync(cancellationToken, item.CountryId);
            var countryName = country?.Shortname ?? "";

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
        response.Pagination = new()
        {
            TotalRecords = hotels.totalRecord,
            CurrentPage = request.PageNumber,
            TotalPages = hotels.TotalPages,
            PrevPage = request.PageNumber > 1 ? request.PageNumber - 1 : null,
            NextPage = request.PageNumber < hotels.TotalPages ? request.PageNumber + 1 : null
        };
        response.CountryList = response.Items
    .Select(p => p.HotelCountry)
    .Where(c => !string.IsNullOrWhiteSpace(c))
    .Distinct()
    .ToList();
        return response;
    }

}