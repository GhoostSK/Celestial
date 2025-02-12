using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tours;

public sealed class AddTourCommandHandler : BaseRequestHandler<AddTourCommandReq, AddTourCommandRes>
{
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ITourService _tourService;

    public AddTourCommandHandler(ITourService tourService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService)
    {
        _tourService = tourService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
    }

    protected override async Task<AddTourCommandRes> HandleCore(AddTourCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        foreach (var item in request.LocalizedProperty)
            if (!languages.Any(p => p.Id == item.LanguageId))
                return new AddTourCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };

        await _tourService.AddAsync(request.Tours, cancellationToken);
        await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);
        var localizedProperties = new List<LocalizedProperty>();

        foreach (var localizedItem in request.LocalizedProperty)
        {
            localizedProperties.AddRange(new[]
            {
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = request.Tours.Id,
                    LocalKey = nameof(localizedItem.Name),
                    LocalValue = localizedItem.Name
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = request.Tours.Id,
                    LocalKey = nameof(localizedItem.TourMainText),
                    LocalValue = localizedItem.TourMainText
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = request.Tours.Id,
                    LocalKey = nameof(localizedItem.PlanShortText),
                    LocalValue = localizedItem.PlanShortText
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = request.Tours.Id,
                    LocalKey = nameof(localizedItem.TourTips),
                    LocalValue = localizedItem.TourTips
                }
            });

            foreach (var facility in localizedItem.TourFacilities)
                localizedProperties.Add(new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = request.Tours.Id,
                    LocalKey = nameof(localizedItem.TourFacilities),
                    LocalValue = facility
                });

            foreach (var (plan, index) in localizedItem.TourPlans.Select((plan, index) => (plan, index)))
            {
                if (request.Tours.TourPlans.Count < index)
                    break;

                localizedProperties.AddRange(new[]
                {
                    new LocalizedProperty
                    {
                        LanguageId = localizedItem.LanguageId,
                        LocalKeyGroup = "TourPlan",
                        EntityId = request.Tours.TourPlans[index].Id,
                        LocalKey = nameof(plan.PlanDayText),
                        LocalValue = plan.PlanDayText
                    },
                    new LocalizedProperty
                    {
                        LanguageId = localizedItem.LanguageId,
                        LocalKeyGroup = "TourPlan",
                        EntityId = request.Tours.TourPlans[index].Id,
                        LocalKey = nameof(plan.PlanText),
                        LocalValue = plan.PlanText
                    }
                });
            }
        }

        await _localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        return new AddTourCommandRes();
    }
}