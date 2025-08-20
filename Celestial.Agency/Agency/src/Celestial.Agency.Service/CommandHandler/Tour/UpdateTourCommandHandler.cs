//using Celestial.ParvazBolourin.Core.DAL.Model.Global;
//using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
//using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
//using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
//using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
//using Celestial.ParvazBolourin.Service.Command.Tour;
//using Mapster;
//using MediatR;
//using MofidApp.SharedKernel.Domain.Services;

//namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

//public class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommandReq, UpdateTourCommandRes>
//{
//    private readonly ILanguageService _languageService;
//    private readonly ILocalizedPropertyService _localizedPropertyService;
//    private readonly ITourPlanService _tourPlanService;
//    private readonly ITourService _tourService;

//    public UpdateTourCommandHandler(ITourService tourService, ILocalizedPropertyService localizedPropertyService,
//        ILanguageService languageService, ITourPlanService tourPlanService)
//    {
//        _tourService = tourService;
//        _localizedPropertyService = localizedPropertyService;
//        _languageService = languageService;
//        _tourPlanService = tourPlanService;
//    }

//    public async Task<UpdateTourCommandRes> Handle(UpdateTourCommandReq request, CancellationToken cancellationToken)
//    {
//        var languages = await _languageService.GetLanguages(cancellationToken);
//        foreach (var item in request.LocalizedProperty)
//            if (!languages.Any(p => p.Id == item.LanguageId))
//                return new UpdateTourCommandRes
//                {
//                    Error = new ValidationError("LA400", "invalid Language")
//                };

//        var tour = await _tourService.FindByIdAsync(cancellationToken, request.Id);
//        if (tour == null)
//            return new UpdateTourCommandRes
//            {
//                Error = new ValidationError("E404", "Tour Not Found")
//            };

//        foreach (var tourPlansUpdate in request.TourPlans)
//            tour.TourPlans.Add(new TourPlans
//            {
//                Id = tourPlansUpdate.Id,
//                PlanPicturePath = tourPlansUpdate.PlanPicturePath
//            });

//        request.Tours.Id = request.Id;
//        request.Tours.Adapt(tour);
//        await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

//        foreach (var tourPlan in request.TourPlans)
//            foreach (var tourPlanLocalize in tourPlan.TourPlanLocalizeData)
//            {
//                var localizePlan = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
//                    new GetLocalizedPropertyReq
//                    {
//                        EntityId = tourPlan.Id,
//                        LanguageId = tourPlanLocalize.LanguageId,
//                        LocalKeyGroup = "TourPlan"
//                    }, cancellationToken);
//                if (localizePlan.Count == 0)
//                {
//                    var plan = await _tourPlanService.FindByIdAsync(cancellationToken, tourPlan.Id);
//                    if (plan != null)
//                    {
//                        await _localizedPropertyService.AddAsync(new LocalizedProperty
//                        {
//                            LanguageId = tourPlanLocalize.LanguageId,
//                            LocalKey = nameof(tourPlanLocalize.PlanDayText),
//                            LocalValue = tourPlanLocalize.PlanDayText,
//                            EntityId = tourPlan.Id,
//                            LocalKeyGroup = "TourPlan"
//                        }, cancellationToken);
//                        await _localizedPropertyService.AddAsync(new LocalizedProperty
//                        {
//                            LanguageId = tourPlanLocalize.LanguageId,
//                            LocalKey = nameof(tourPlanLocalize.PlanText),
//                            LocalValue = tourPlanLocalize.PlanText,
//                            EntityId = tourPlan.Id,
//                            LocalKeyGroup = "TourPlan"
//                        }, cancellationToken);
//                    }

//                    break;
//                }

//                var planDayText = await _localizedPropertyService.FindByIdAsync(cancellationToken,
//                    localizePlan.FirstOrDefault(p => p.LocalKey == nameof(tourPlanLocalize.PlanDayText))!.Id);
//                var planText = await _localizedPropertyService.FindByIdAsync(cancellationToken,
//                    localizePlan.FirstOrDefault(p => p.LocalKey == nameof(tourPlanLocalize.PlanText))!.Id);
//                planDayText.LocalValue = tourPlanLocalize.PlanDayText;
//                planText.LocalValue = tourPlanLocalize.PlanText;
//            }

//        foreach (var localize in request.LocalizedProperty)
//        {
//            var localizedData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
//                new GetLocalizedPropertyReq
//                {
//                    LanguageId = localize.LanguageId,
//                    LocalKeyGroup = "Tour",
//                    EntityId = request.Id
//                }, cancellationToken);

//            foreach (var property in localizedData)
//            {
//                var existingProperty = await _localizedPropertyService.FindByIdAsync(cancellationToken, property.Id);

//                switch (property.LocalKey)
//                {
//                    case nameof(localize.Name):
//                        existingProperty.LocalValue = localize.Name;
//                        break;
//                    case nameof(localize.TourMainText):
//                        existingProperty.LocalValue = localize.TourMainText;
//                        break;
//                    case nameof(localize.TourFacilities):
//                        existingProperty.LocalValue = string.Join(",", localize.TourFacilities);
//                        break;
//                    case nameof(localize.TourTips):
//                        existingProperty.LocalValue = localize.TourTips;
//                        break;
//                    case nameof(localize.PlanShortText):
//                        existingProperty.LocalValue = localize.PlanShortText;
//                        break;
//                }
//            }
//        }

//        await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);
//        return new UpdateTourCommandRes();
//    }
//}

