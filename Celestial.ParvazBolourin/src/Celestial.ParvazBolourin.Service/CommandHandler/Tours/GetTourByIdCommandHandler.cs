using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tours;

public class GetTourByIdCommandHandler : IRequestHandler<GetTourByIdCommandReq, GetTourByIdCommandRes>
{
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ITourService _tourService;


    public GetTourByIdCommandHandler(ITourService tourService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _tourService = tourService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    public async Task<GetTourByIdCommandRes> Handle(GetTourByIdCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (!languages.Any(p => p.Id == request.LanguageId))
            return new GetTourByIdCommandRes
            {
                Error = new ValidationError("LA400", "invalid Language")
            };

        var tour = await _tourService.GetTourById(request.TourId, cancellationToken);
        if (tour == null)
            return new GetTourByIdCommandRes
            {
                Error = new ValidationError("T404", "Tour Not Found")
            };

        var localizedData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq
        {
            EntityId = tour.Id,
            LanguageId = request.LanguageId,
            LocalKeyGroup = "Tour"
        }, cancellationToken);

        var itemResponse = tour.Adapt<GetTourByIdItemRes>();
        itemResponse.LanguageId = request.LanguageId;
        itemResponse.Name =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.Name))?.LocalValue ??
            string.Empty;
        itemResponse.TourMainText =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.TourMainText))?.LocalValue ??
            string.Empty;
        itemResponse.TourTips =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.TourTips))?.LocalValue ??
            string.Empty;
        itemResponse.PlanShortText =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdItemRes.PlanShortText))?.LocalValue ??
            string.Empty;
        itemResponse.TourFacilities = localizedData
            .Where(p => p.LocalKey == nameof(GetTourByIdItemRes.TourFacilities))
            .Select(p => p.LocalValue)
            .ToList();


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
            var data = new GetTourByIdTourPlan
            {
                Id = tour.TourPlans[i].Id,
                PlanDayText =
                    tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdTourPlan.PlanDayText))!
                        .LocalValue,
                PlanText = tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == nameof(GetTourByIdTourPlan.PlanText))!
                    .LocalValue,
                PlanPicturePath = tour.TourPlans[i].PlanPicturePath
            };
            itemResponse.TourPlans[i] = data;
        }

        return new GetTourByIdCommandRes
        {
            Item = itemResponse
        };
    }
}