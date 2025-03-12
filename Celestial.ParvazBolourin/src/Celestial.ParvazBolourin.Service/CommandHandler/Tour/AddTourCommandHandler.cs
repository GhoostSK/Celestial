using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public sealed class AddTourCommandHandler : BaseRequestHandler<AddTourCommandReq, AddTourCommandRes>
{
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly ITourService _tourService;
    private readonly IImageService _imageService;


    public AddTourCommandHandler(ITourService tourService, ILocalizedPropertyService localizedPropertyService,
        ILanguageService languageService, IImageService imageService)
    {
        _tourService = tourService;
        _localizedPropertyService = localizedPropertyService;
        _languageService = languageService;
        _imageService = imageService;
    }

    protected override async Task<AddTourCommandRes> HandleCore(AddTourCommandReq request, CancellationToken cancellationToken)
    {
        // اعتبارسنجی زبان‌ها
        var languages = await _languageService.GetLanguages(cancellationToken);
        if (request.LocalizedProperty.Any(item => !languages.Any(p => p.Id == item.LanguageId)))
            return new AddTourCommandRes { Error = new ValidationError("LA400", "Invalid Language") };

        // افزودن تور
        var tour = request.Tours.Adapt<Tours>();
        await _tourService.AddAsync(tour, cancellationToken);
        await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        // افزودن تصویر تور
        var tourImage = new Images
        {
            PicturePath = request.Tours.PicturePath,
            DisplayOrder = 0,
            EntityId = tour.Id,
            KeyGroup = "Tour"
        };
        await _imageService.AddAsync(tourImage, cancellationToken);
        await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var updateTour = await _tourService.GetTourById(tour.Id, cancellationToken: cancellationToken);
        updateTour!.PictureId = tourImage.Id;
        await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        // افزودن تصاویر پلن‌های تور
        for (int i = 0; i < request.Tours.TourPlans.Count; i++)
        {
            var plan = request.Tours.TourPlans[i];
            var planPictureIds = new List<int>();

            foreach (var picturePath in plan.PlanPicturePath)
            {
                var planImage = new Images
                {
                    PicturePath = picturePath,
                    DisplayOrder = 0,
                    EntityId = updateTour.TourPlans[i].Id,
                    KeyGroup = "TourPlan"
                };
                await _imageService.AddAsync(planImage, cancellationToken);
                await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

                planPictureIds.Add((int)planImage.Id);
            }

            updateTour.TourPlans[i].PlanPictureId = planPictureIds;
            await _tourService.UnitOfWork.SaveChangesAsync(cancellationToken);
        }



        var localizedProperties = new List<LocalizedProperty>();
        foreach (var localizedItem in request.LocalizedProperty)
        {
            localizedProperties.AddRange(new[]
            {
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.Name),
                    LocalValue = localizedItem.Name
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.TourMainText),
                    LocalValue = localizedItem.TourMainText
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.PlanShortText),
                    LocalValue = localizedItem.PlanShortText
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Tour",
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.TourTips),
                    LocalValue = localizedItem.TourTips
                }
            });

            // افزودن تسهیلات تور
            localizedProperties.AddRange(localizedItem.TourFacilities.Select(facility => new LocalizedProperty
            {
                LanguageId = localizedItem.LanguageId,
                LocalKeyGroup = "Tour",
                EntityId = tour.Id,
                LocalKey = nameof(localizedItem.TourFacilities),
                LocalValue = facility
            }));

            // افزودن پلن‌های تور
            foreach (var (plan, index) in localizedItem.TourPlans.Select((plan, index) => (plan, index)))
            {
                if (index >= tour.TourPlans.Count)
                    break;

                localizedProperties.AddRange(new[]
                {
                    new LocalizedProperty
                    {
                        LanguageId = localizedItem.LanguageId,
                        LocalKeyGroup = "TourPlan",
                        EntityId = tour.TourPlans[index].Id,
                        LocalKey = nameof(plan.PlanDayText),
                        LocalValue = plan.PlanDayText
                    },
                    new LocalizedProperty
                    {
                        LanguageId = localizedItem.LanguageId,
                        LocalKeyGroup = "TourPlan",
                        EntityId = tour.TourPlans[index].Id,
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
