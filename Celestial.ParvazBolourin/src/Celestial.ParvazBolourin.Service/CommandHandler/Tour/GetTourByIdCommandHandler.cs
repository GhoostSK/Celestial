using Celestial.ParvazBolourin.Core.DAL.Repositories.Geo;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public class GetTourByIdCommandHandler : IRequestHandler<GetTourByIdCommandReq, GetTourByIdCommandRes>
{
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ITourService _tourService;
    private readonly IImageService _imageService;
    private readonly ICountryService _countryService;

    public GetTourByIdCommandHandler(ITourService tourService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService, IImageService imageService, ICountryService countryService)
    {
        _tourService = tourService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
        _imageService = imageService;
        _countryService = countryService;
    }

    public async Task<GetTourByIdCommandRes> Handle(GetTourByIdCommandReq request, CancellationToken cancellationToken)
    {

        var tour = new Core.DAL.Model.Tour.Tours();

        if (request.TourId > 0)
        {
            var languages = await _languageService.GetLanguages(cancellationToken);
            if (!languages.Any(p => p.Id == request.LanguageId))
                return new GetTourByIdCommandRes
                {
                    Error = new ValidationError("LA400", "Invalid Language")
                };

            tour = await _tourService.GetTourById(request.TourId, cancellationToken);
        }
        else if (!string.IsNullOrWhiteSpace(request.Slug))
        {
            var localizedProperty = await _localizedPropertyService.FirstOrDefaultAsync(p =>
                p.LocalKey == "Slug" &&
                p.LocalValue == request.Slug &&
                p.LocalKeyGroup == "Tour", cancellationToken: cancellationToken);

            if (localizedProperty == null)
            {
                return new GetTourByIdCommandRes
                {
                    Error = new ValidationError("T404", "Tour not found with provided slug")
                };
            }

            tour = await _tourService.GetTourById(localizedProperty.EntityId, cancellationToken);
            request.LanguageId = localizedProperty.LanguageId;
            request.TourId = localizedProperty.EntityId;
        }

        if (tour == null)
            return new GetTourByIdCommandRes
            {
                Error = new ValidationError("T404", "Tour Not Found")
            };

        var tourImage = await _imageService.FirstOrDefaultAsync(p => p.EntityId == tour.Id, cancellationToken: cancellationToken);
        var localizedData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq
        {
            EntityId = tour.Id,
            LanguageId = request.LanguageId,
            LocalKeyGroup = "Tour"
        }, cancellationToken);

        var itemResponse = tour.Adapt<GetTourByIdItemRes>();

        foreach (var country in tour.CountriesId)
        {
            var countryData = await _countryService.FindByIdAsync(cancellationToken, country);
            itemResponse.Countries.Add(countryData.Name);
        }

        itemResponse.LanguageId = request.LanguageId;
        itemResponse.PicturePath = tourImage?.PicturePath ?? string.Empty;
        itemResponse.Name = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.Name))?.LocalValue ?? string.Empty;
        itemResponse.TourMainText = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.TourMainText))?.LocalValue ?? string.Empty;
        itemResponse.TourTips = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.TourTips))?.LocalValue ?? string.Empty;
        itemResponse.PlanShortText = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.PlanShortText))?.LocalValue ?? string.Empty;
        itemResponse.TourFacilities = localizedData.Where(p => p.LocalKey == nameof(GetTourByIdItemRes.TourFacilities)).Select(p => p.LocalValue).ToList();
        itemResponse.Slug = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.Slug))?.LocalValue ?? string.Empty;
        itemResponse.MetaDescription = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.MetaDescription))?.LocalValue ?? string.Empty;
        itemResponse.MetaKeywords = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.MetaKeywords))?.LocalValue ?? string.Empty;
        itemResponse.MetaTitle = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.MetaTitle))?.LocalValue ?? string.Empty;

        for (var i = 0; i < tour.TourPlans.Count; i++)
        {
            var tourPlanLocalizeData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = tour.TourPlans[i].Id,
                    LanguageId = request.LanguageId,
                    LocalKeyGroup = "TourPlan"
                }, cancellationToken);

            if (!tourPlanLocalizeData.Any())
                break;

            var planImages = await _imageService.GetImagesByEntityId(tour.TourPlans[i].Id, "TourPlan", cancellationToken);
            var planPicturePaths = planImages.Select(p => p.PicturePath).ToList();

            itemResponse.TourPlans[i].PlanText = tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == "PlanText")?.LocalValue ?? string.Empty;
            itemResponse.TourPlans[i].PlanDayText = tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == "PlanDayText")?.LocalValue ?? string.Empty;
            itemResponse.TourPlans[i].PlanPicturePath = planPicturePaths;
        }

        return new GetTourByIdCommandRes
        {
            Item = itemResponse
        };
    }
}
