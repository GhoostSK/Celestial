using Celestial.ParvazBolourin.Core.DAL.Model.Global;
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
    ILanguageService languageService)
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

            result.Add(new GetTourFeaturedItemDto
            {
                BasePrice = tour.BasePrice,
                PictureId = tour.PictureId,
                StartDate = tour.StartDate,
                CountriesId = tour.CountriesId,
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
