using Celestial.Agency.Core.DAL.Model.Global;
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

            var name = localizedName.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty;
            var picture = await imageService.FirstOrDefaultAsync(
                p => p.EntityId == tour.Id && p.KeyGroup == "Tour" && p.DisplayOrder == 0,
                cancellationToken: cancellationToken);
            var tourCountries = new List<string>();
            //foreach (var countryId in tour.Countries)
            //{
            //    var country = await countryService.FindByIdAsync(cancellationToken, countryId);
            //    tourCountries.Add(country.Shortname);
            //}

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