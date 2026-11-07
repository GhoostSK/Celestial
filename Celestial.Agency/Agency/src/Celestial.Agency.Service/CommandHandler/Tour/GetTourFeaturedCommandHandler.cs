using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.Tour;
using Celestial.SharedKernel.Common;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Tour;

public class GetTourFeaturedCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService,
    IImageService imageService,
    ICountryService countryService)
    : IRequestHandler<GetTourFeaturedCommandReq, GetTourFeaturedCommandRes>
{
    public async Task<GetTourFeaturedCommandRes> Handle(GetTourFeaturedCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (languages.All(p => p.LanguageCulture.Split('-')[0] != request.Language))
            return new GetTourFeaturedCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        var topTours = await tourService.GetTopToursByViews(request.TopCount, cancellationToken);

        var countries = await countryService.GetCountryList(cancellationToken);
        var result = new List<GetTourFeaturedItemDto>();

        foreach (var tour in topTours)
        {
            var localizedName = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = tour.Id,
                    Language = request.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour
                },
                cancellationToken
            );

            var name = localizedName.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Name))
                ?.LocalValue ?? string.Empty;
            var slug = localizedName.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Slug))
                ?.LocalValue ?? string.Empty;
            var picture = await imageService.FirstOrDefaultAsync(
                p => p.EntityId == tour.Id && p.KeyGroup == "Tour" && p.DisplayOrder == 0,
                cancellationToken: cancellationToken);

            result.Add(new GetTourFeaturedItemDto
            {
                Id = tour.Id,
                BasePrice = tour.BasePrice,
                PicturePath = picture?.PicturePath ?? string.Empty,
                StartDate = tour.StartDate,
                Countries = countries
                    .Where(c => tour.Countries.Contains(c.Id))
                    .Select(c => c.Name)
                    .ToList(),
                Title = name,
                ForeignCurrency = tour.ForeignCurrency,
                ForeignPrice = tour.ForeignPrice,
                DaysUntilRegistrationDeadline =
                    tour.RegistrationDeadline.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber,
                TotalDay = tour.TotalDay,
                Score = tour.Score,
                Slug = slug
            });
        }

        return new GetTourFeaturedCommandRes
        {
            Items = result
        };
    }
}
