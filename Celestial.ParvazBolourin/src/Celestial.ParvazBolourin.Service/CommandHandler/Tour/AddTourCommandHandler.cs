using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Exchange;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Celestial.ParvazBolourin.Service.Command.Tour;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour;

public sealed class AddTourCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ILanguageService languageService,
    ICurrencyService currencyService,
    IImageService imageService,
    ITourEquipmentsService iEquipmentsService)
    : BaseRequestHandler<AddTourCommandReq, AddTourCommandRes>
{
    protected override async Task<AddTourCommandRes> HandleCore(AddTourCommandReq request, CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        if (request.LocalizedProperty.Any(item => languages.All(p => p.Id != item.LanguageId)))
            return new AddTourCommandRes { Error = new ValidationError("LA400", "Invalid Language") };
        foreach (var localizeSlug in request.LocalizedProperty)
        {
            var checkSlug = await localizedPropertyService.FirstOrDefaultAsync(p => p.LocalKeyGroup == LocalKeyGroupType.Tour && p.LocalKey == "Slug" && p.LocalValue == localizeSlug.Slug && p.LanguageId == localizeSlug.LanguageId, cancellationToken: cancellationToken);
            if (checkSlug != null)
            {
                return new AddTourCommandRes()
                {
                    Error = ValidationError.UnhandledValidationError("S403", $"Slug Already exist({checkSlug.LanguageId})")
                };
            }
        }


        if (request.Tours.ForeignCurrency != string.Empty && await currencyService.FirstOrDefaultAsync(p => p.Name == request.Tours.ForeignCurrency,
                cancellationToken: cancellationToken) == null)
        {
            return new AddTourCommandRes()
            {
                Error = ValidationError.UnhandledValidationError("F403", $"ForeignCurrency is not valid")
            };
        }

        request.Tours.NecessaryEquipment = request.Tours.NecessaryEquipment.Distinct().ToList();
        request.Tours.UnNecessaryEquipment = request.Tours.UnNecessaryEquipment.Distinct().ToList();
        bool hasCommon = request.Tours.NecessaryEquipment.Intersect(request.Tours.UnNecessaryEquipment).Any();
        if (hasCommon)
        {
            return new()
            {
                Error = ValidationError.UnhandledValidationError("E403",
                    "an item cannot be marked as both required and optional")
            };
        }

        foreach (var item in request.Tours.NecessaryEquipment)
        {
            var isValid = await iEquipmentsService.FirstOrDefaultAsync(p => p.Id == item, cancellationToken: cancellationToken);
            if (isValid == null)
            {
                return new()
                {
                    Error = ValidationError.UnhandledValidationError("E403",
                        $"NecessaryEquipment Not Valid {item}")
                };
            }
        }

        foreach (var item in request.Tours.UnNecessaryEquipment)
        {
            var isValid = await iEquipmentsService.FirstOrDefaultAsync(p => p.Id == item, cancellationToken: cancellationToken);
            if (isValid == null)
            {
                return new()
                {
                    Error = ValidationError.UnhandledValidationError("E403",
                        $"UnNecessaryEquipment Not Valid {item}")
                };
            }
        }
        var tour = request.Tours.Adapt<Tours>();
        tour.TourCode = await tourService.GenerateUniqueTourCodeAsync(cancellationToken);
        await tourService.AddAsync(tour, cancellationToken);
        await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var tourImage = new Images
        {
            PicturePath = request.Tours.PicturePath,
            DisplayOrder = 0,
            EntityId = tour.Id,
            KeyGroup = "Tour"
        };
        await imageService.AddAsync(tourImage, cancellationToken);
        await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var updateTour = await tourService.GetTourById(tour.Id, cancellationToken: cancellationToken);
        updateTour!.PictureId = tourImage.Id;
        tour.TotalDay = request.Tours.TourPlans.Count;
        await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

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
                await imageService.AddAsync(planImage, cancellationToken);
                await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

                planPictureIds.Add((int)planImage.Id);
            }

            updateTour.TourPlans[i].PlanPictureId = planPictureIds;

            await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        var localizedProperties = new List<LocalizedProperty>();
        foreach (var localizedItem in request.LocalizedProperty)
        {
            localizedProperties.AddRange(new[]
            {
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.Name),
                    LocalValue = localizedItem.Name
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.TourMainText),
                    LocalValue = localizedItem.TourMainText
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.PlanShortText),
                    LocalValue = localizedItem.PlanShortText
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.TourTips),
                    LocalValue = localizedItem.TourTips
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.MetaKeywords),
                    LocalValue = localizedItem.MetaKeywords
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.MetaTitle),
                    LocalValue = localizedItem.MetaTitle
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.Slug),
                    LocalValue = localizedItem.Slug
                },
                new LocalizedProperty
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.MetaDescription),
                    LocalValue = localizedItem.MetaDescription
                },
            });

            localizedProperties.AddRange(localizedItem.TourFacilities.Select(facility => new LocalizedProperty
            {
                LanguageId = localizedItem.LanguageId,
                LocalKeyGroup = LocalKeyGroupType.Tour,
                EntityId = tour.Id,
                LocalKey = nameof(localizedItem.TourFacilities),
                LocalValue = facility
            }));

            foreach (var (plan, index) in localizedItem.TourPlans.Select((plan, index) => (plan, index)))
            {
                if (index >= tour.TourPlans.Count)
                    break;

                localizedProperties.AddRange(new[]
                {
                    new LocalizedProperty
                    {
                        LanguageId = localizedItem.LanguageId,
                        LocalKeyGroup = LocalKeyGroupType.TourPlan,
                        EntityId = tour.TourPlans[index].Id,
                        LocalKey = nameof(plan.PlanDayText),
                        LocalValue = plan.PlanDayText
                    },
                    new LocalizedProperty
                    {
                        LanguageId = localizedItem.LanguageId,
                        LocalKeyGroup = LocalKeyGroupType.TourPlan,
                        EntityId = tour.TourPlans[index].Id,
                        LocalKey = nameof(plan.PlanText),
                        LocalValue = plan.PlanText
                    }
                });
            }
        }

        await localizedPropertyService.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        return new AddTourCommandRes();
    }
}
