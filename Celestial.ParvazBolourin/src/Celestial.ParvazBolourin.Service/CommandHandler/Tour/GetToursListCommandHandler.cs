using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Celestial.Service.Command.Tour;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public sealed class GetToursListCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ICountryService countryService,
    ILanguageService languageService, IImageService imageService)
    : BaseRequestHandler<GetToursListCommandReq, GetToursListCommandRes>
{
    protected override async Task<GetToursListCommandRes> HandleCore(GetToursListCommandReq request,
     CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (languages.Any(p => p.LanguageCulture.Split('-')[0] == request.Language))
            return new GetToursListCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var response = new GetToursListCommandRes();
        var tourData = await tourService.GetToursList(request.Adapt<GetTourListReq>(), cancellationToken);

        if (!tourData.Items.Any())
            return new GetToursListCommandRes
            {
                Error = new ValidationError("T404", "Tour Not Found")
            };

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
            var slug = localizeData.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Slug))?.LocalValue;
            var picture = await imageService.FirstOrDefaultAsync(p => p.EntityId == item.Id && p.KeyGroup == "Tour" && p.DisplayOrder == 0, cancellationToken: cancellationToken);

            var countries = new List<string>();
            foreach (var country in item.CountriesId)
            {
                var countryData = await countryService.FindByIdAsync(cancellationToken, country);
                countries.Add(countryData.Shortname);
            }
            data.Add(new GetToursListItemRes
            {
                Id = item.Id,
                Score = item.Score,
                Name = name!,
                StartDate = item.StartDate,
                TourText = tourText!,
                PicturePath = picture.PicturePath,
                Country = countries,
                TourStatus = item.TourStatus,
                Slug = slug!,
                DaysUntilRegistrationDeadline = item.TourStatus == TourStatus.Planning
                    ? null
                    : item.RegistrationDeadline.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber,
                RegistrationDeadline = item.TourStatus == TourStatus.Planning
                    ? null
                    : item.RegistrationDeadline,
                ForeignCurrency = item.ForeignCurrency,
                ForeignPrice = item.ForeignPrice,
                BasePrice = item.BasePrice,
                TotalDay = item.TotalDay
            });

        }
        //TEMP
        if (!string.IsNullOrEmpty(request.Title))
        {
            var filtredData = data.Where(p => p.Name.Contains(request.Title)).ToList();
            response.Items = filtredData;
        }
        else
        {
            response.Items = data;
        }


        response.Pagination = new()
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