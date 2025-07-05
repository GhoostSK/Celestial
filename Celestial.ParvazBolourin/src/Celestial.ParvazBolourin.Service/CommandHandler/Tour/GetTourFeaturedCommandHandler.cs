using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Celestial.ParvazBolourin.Service.Command.Tour;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public class GetTourFeaturedCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService, IImageService imageService, ICountryService countryService)

    : IRequestHandler<GetTourFeaturedCommandReq, GetTourFeaturedCommandRes>
{
    public async Task<GetTourFeaturedCommandRes> Handle(GetTourFeaturedCommandReq request, CancellationToken cancellationToken)
    {

        var languages = await languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetTourFeaturedCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        var topTours = await tourService.GetTopToursByViews(request.TopCount, cancellationToken);

        var result = new List<GetTourFeaturedItemDto>();

        foreach (var tour in topTours)
        {
            var localizedName = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = tour.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour
                },
                cancellationToken
            );

            var name = localizedName.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty;
            var picture = await imageService.FirstOrDefaultAsync(p => p.EntityId == tour.Id && p.KeyGroup == "Tour" && p.DisplayOrder == 0, cancellationToken: cancellationToken);
            var tourCountries = new List<string>();
            foreach (var countryId in tour.CountriesId)
            {
                var country = await countryService.FindByIdAsync(cancellationToken: cancellationToken, countryId);
                tourCountries.Add(country.Shortname);
            }
            result.Add(new GetTourFeaturedItemDto
            {
                BasePrice = tour.BasePrice,
                PicturePath = picture.PicturePath,
                StartDate = tour.StartDate,
                Countries = tourCountries,
                Title = name,
                ForeignCurrency = tour.ForeignCurrency,
                ForeignPrice = tour.ForeignPrice
            });
        }

        return new GetTourFeaturedCommandRes
        {
            Items = result
        };
    }
}
