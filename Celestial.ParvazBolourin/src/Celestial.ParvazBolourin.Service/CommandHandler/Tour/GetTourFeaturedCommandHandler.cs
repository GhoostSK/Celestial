using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Celestial.ParvazBolourin.Service.Command.Tour;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public class GetTourFeaturedCommandHandler : IRequestHandler<GetTourFeaturedCommandReq, GetTourFeaturedCommandRes>
{
    private readonly ITourService _tourService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ILanguageService _languageService;


    public GetTourFeaturedCommandHandler(ITourService tourService, ILocalizedPropertyService localizedPropertyService, ILanguageService languageService)
    {
        _tourService = tourService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    public async Task<GetTourFeaturedCommandRes> Handle(GetTourFeaturedCommandReq request, CancellationToken cancellationToken)
    {

        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetTourFeaturedCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };
        var topTours = await _tourService.GetTopToursByViews(request.TopCount, cancellationToken);

        var result = new List<GetTourFeaturedItemDto>();

        foreach (var tour in topTours)
        {
            var localizedName = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = tour.Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "Tour"
                },
                cancellationToken
            );

            var name = localizedName.FirstOrDefault(p => p.LocalKey == "Name")?.LocalValue ?? string.Empty;

            result.Add(new GetTourFeaturedItemDto
            {
                Price = tour.Price,
                PictureId = tour.PictureId,
                StartDate = tour.StartDate,
                CountriesId = tour.CountriesId,
                Name = name
            });
        }

        return new GetTourFeaturedCommandRes
        {
            Data = result
        };
    }
}
