using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Hotel;
using Celestial.Agency.Core.DAL.Repositories.Hotel.Model;
using Celestial.Agency.Service.Command.Hotel;
using Celestial.Agency.Service.Command.localization;
using Celestial.SharedKernel.Mediator;
using Mapster;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Hotel;

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
        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new GetHotelsListCommandRes
            {
                Error = validateLanguage.Error
            };

        var response = new GetHotelsListCommandRes();
        var allCountries = await countryService.GetCountryList(cancellationToken);

        // فیلتر کشور با نام/کد کشور می‌آید و باید به شناسه تبدیل شود
        // (نگاشت خودکار نام را به عدد تبدیل می‌کرد و خطا می‌داد).
        var listReq = new GetHotelsListReq
        {
            Title = request.Title,
            HotelLocation = request.HotelLocation,
            HotelContinent = request.HotelContinent,
            FromPrice = request.FromPrice,
            ToPrice = request.ToPrice,
            HotelCountry = allCountries
                .Where(c => request.HotelCountry.Contains(c.Name, StringComparer.OrdinalIgnoreCase)
                            || request.HotelCountry.Contains(c.Shortname, StringComparer.OrdinalIgnoreCase))
                .Select(c => c.Id)
                .ToList(),
            HotelScore = request.HotelScore,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var hotels = await hotel.GetHotelsList(listReq, cancellationToken);

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
            var countryName = country?.Name ?? string.Empty;

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

        response.Pagination = new PaginationMeta
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