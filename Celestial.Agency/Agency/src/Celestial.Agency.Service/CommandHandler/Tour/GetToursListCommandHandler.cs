using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Agency.Core.DAL.Repositories.Booking;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Core.Enum.Tour;
using Celestial.Agency.Service.Command.localization;
using Celestial.Agency.Service.Command.Tour;
using Celestial.SharedKernel.Mediator;
using Mapster;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Tour;

public sealed class GetToursListCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ICountryService countryService,
    ITourBookingService tourBookingService,
    IImageService imageService,
    IMediator mediator)
    : BaseRequestHandler<GetToursListCommandReq, GetToursListCommandRes>
{
    protected override async Task<GetToursListCommandRes> HandleCore(GetToursListCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string> { request.Language };
        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new GetToursListCommandRes
            {
                Error = validateLanguage.Error
            };

        var response = new GetToursListCommandRes();
        var countries = await countryService.GetCountryList(cancellationToken);

        // درخواست به‌صورت صریح ساخته می‌شود؛ فیلتر کشور با «نام» می‌آید و باید به شناسه تبدیل شود
        // (نگاشت خودکار، نام کشور را به عدد تبدیل می‌کرد و خطا می‌داد).
        var listReq = new GetTourListReq
        {
            Countries = countries
                .Where(c => request.Countries.Contains(c.Name, StringComparer.OrdinalIgnoreCase))
                .Select(c => c.Id)
                .ToList(),
            Cities = request.Cities
                .Select(city => long.TryParse(city, out var cityId) ? cityId : 0)
                .Where(cityId => cityId != 0)
                .ToList(),
            FromDay = request.FromDay,
            ToDay = request.ToDay,
            FromPrice = (decimal)request.FromPrice,
            ToPrice = (decimal)request.ToPrice,
            TourContinent = request.TourContinent,
            TourType = request.TourType,
            TourScope = request.TourScope,
            IsActiveTour = request.IsActiveTour,
            SortBy = request.SortBy,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var tourData = await tourService.GetToursList(listReq, cancellationToken);

        // یک لیست خالی خطا نیست؛ صفحه‌ی فرانت باید حالت «نتیجه‌ای یافت نشد» را نمایش دهد
        if (!tourData.Items.Any())
            return response;

        var data = new List<GetToursListItemRes>();

        foreach (var item in tourData.Items)
        {
            var localizeData = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour
                }, cancellationToken);

            var name = localizeData.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Name))
                ?.LocalValue;
            var tourText = localizeData
                .FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.TourMainText))?.LocalValue;
            var slug = localizeData.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Slug))
                ?.LocalValue;
            var picture = await imageService.FirstOrDefaultAsync(
                p => p.EntityId == item.Id && p.KeyGroup == "Tour" && p.DisplayOrder == 0,
                cancellationToken: cancellationToken);

            var reservedSeats = await tourBookingService.GetReservedSeatCount(item.Id, cancellationToken);
            var remainingCapacity = item.PassengerCapacity - reservedSeats;

            data.Add(new GetToursListItemRes
            {
                Id = item.Id,
                Score = item.Score,
                Name = name ?? string.Empty,
                StartDate = item.StartDate,
                TourText = tourText ?? string.Empty,
                PicturePath = picture?.PicturePath ?? string.Empty,
                Country = countries
                    .Where(c => item.Countries.Contains(c.Id))
                    .Select(c => c.Name)
                    .ToList(),
                TourStatus = item.TourStatus,
                Slug = slug ?? string.Empty,
                DaysUntilRegistrationDeadline = item.TourStatus == TourStatus.Planning
                    ? null
                    : item.RegistrationDeadline.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber,
                RegistrationDeadline = item.TourStatus == TourStatus.Planning
                    ? null
                    : item.RegistrationDeadline,
                RemainingCapacity = remainingCapacity < 0 ? 0 : remainingCapacity,
                ForeignCurrency = item.ForeignCurrency,
                ForeignPrice = item.ForeignPrice,
                BasePrice = item.BasePrice,
                TotalDay = item.TotalDay
            });
        }

        //TEMP
        response.Items = !string.IsNullOrEmpty(request.Title)
            ? data.Where(p => p.Name.Contains(request.Title)).ToList()
            : data;

        response.Pagination = new PaginationMeta
        {
            TotalRecords = tourData.totalRecord,
            CurrentPage = request.PageNumber,
            TotalPages = tourData.TotalPages,
            PrevPage = request.PageNumber > 1 ? request.PageNumber - 1 : null,
            NextPage = request.PageNumber < tourData.TotalPages ? request.PageNumber + 1 : null
        };

        response.Filters = new TourFiltersDto
        {
            Countries = data.SelectMany(p => p.Country).Distinct().ToList(),
            TourContinent = tourData.Items.Select(p => p.TourContinent).Distinct().ToList(),
            TourType = tourData.Items.Select(p => p.TourType).Distinct().ToList(),
            TourScope = tourData.Items.Select(p => p.TourScope).Distinct().ToList(),
            FromDay = tourData.Items.Min(t => t.TotalDay),
            ToDay = tourData.Items.Max(t => t.TotalDay),
            FromPrice = (double)tourData.Items.Min(t => t.BasePrice),
            ToPrice = (double)tourData.Items.Max(t => t.BasePrice),
            IsActiveTour = [true, false]
        };

        return response;
    }
}
