using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Agency.Core.DAL.Repositories.Exchange;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.localization;
using Celestial.Agency.Service.Command.Tour;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;
using Mapster;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Tour;

public sealed class AddTourCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    ICurrencyService currencyService,
    IImageService imageService,
    ITourEquipmentsService iEquipmentsService,
    IMediator mediator)
    : BaseRequestHandler<AddTourCommandReq, AddTourCommandRes>
{
    protected override async Task<AddTourCommandRes> HandleCore(AddTourCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string>();
        foreach (var lang in request.LocalizedProperty)
        {
            lang.Language = lang.Language?.ToLowerInvariant()!;
            langs.Add(lang.Language);
        }

        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);

        if (validateLanguage.HasError)
            return new AddTourCommandRes
            {
                Error = validateLanguage.Error
            };
        foreach (var localizeSlug in request.LocalizedProperty)
        {
            var checkSlug = await localizedPropertyService.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.Tour && p.LocalKey == "Slug" &&
                     p.LocalValue == localizeSlug.Slug && p.Language == localizeSlug.Language,
                cancellationToken: cancellationToken);
            if (checkSlug != null)
                return new AddTourCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("S403",
                        $"Slug Already exist({checkSlug.Language})")
                };
        }

        if (request.Tours.ForeignCurrency != string.Empty && await currencyService.FirstOrDefaultAsync(
                p => p.Name == request.Tours.ForeignCurrency,
                cancellationToken: cancellationToken) == null)
            return new AddTourCommandRes
            {
                Error = ValidationError.UnhandledValidationError("F403", "ForeignCurrency is not valid")
            };

        request.Tours.NecessaryEquipment = request.Tours.NecessaryEquipment.Distinct().ToList();
        request.Tours.UnNecessaryEquipment = request.Tours.UnNecessaryEquipment.Distinct().ToList();
        var hasCommon = request.Tours.NecessaryEquipment.Intersect(request.Tours.UnNecessaryEquipment).Any();
        if (hasCommon)
            return new AddTourCommandRes
            {
                Error = ValidationError.UnhandledValidationError("E403",
                    "an item cannot be marked as both required and optional")
            };

        foreach (var item in request.Tours.NecessaryEquipment)
        {
            var isValid =
                await iEquipmentsService.FirstOrDefaultAsync(p => p.Id == item, cancellationToken: cancellationToken);
            if (isValid == null)
                return new AddTourCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("E403",
                        $"NecessaryEquipment Not Valid {item}")
                };
        }

        foreach (var item in request.Tours.UnNecessaryEquipment)
        {
            var isValid =
                await iEquipmentsService.FirstOrDefaultAsync(p => p.Id == item, cancellationToken: cancellationToken);
            if (isValid == null)
                return new AddTourCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("E403",
                        $"UnNecessaryEquipment Not Valid {item}")
                };
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

        var updateTour = await tourService.GetTourById(tour.Id, cancellationToken);
        updateTour!.PictureId = tourImage.Id;
        tour.TotalDay = request.Tours.TourPlans.Count;
        await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        for (var i = 0; i < request.Tours.TourPlans.Count; i++)
        {
            var plan = request.Tours.TourPlans[i];
            var planPictureIds = new List<long>();

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
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.Name),
                    LocalValue = localizedItem.Name
                },
                new LocalizedProperty
                {
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.TourMainText),
                    LocalValue = localizedItem.TourMainText
                },
                new LocalizedProperty
                {
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.PlanShortText),
                    LocalValue = localizedItem.PlanShortText
                },
                new LocalizedProperty
                {
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.TourTips),
                    LocalValue = localizedItem.TourTips
                },
                new LocalizedProperty
                {
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.MetaKeywords),
                    LocalValue = localizedItem.MetaKeywords
                },
                new LocalizedProperty
                {
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.MetaTitle),
                    LocalValue = localizedItem.MetaTitle
                },
                new LocalizedProperty
                {
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.Slug),
                    LocalValue = localizedItem.Slug
                },
                new LocalizedProperty
                {
                    Language = localizedItem.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = nameof(localizedItem.MetaDescription),
                    LocalValue = localizedItem.MetaDescription
                }
            });

            localizedProperties.AddRange(localizedItem.TourFacilities.Select(facility => new LocalizedProperty
            {
                Language = localizedItem.Language,
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
                        Language = localizedItem.Language,
                        LocalKeyGroup = LocalKeyGroupType.TourPlan,
                        EntityId = tour.TourPlans[index].Id,
                        LocalKey = nameof(plan.PlanDayText),
                        LocalValue = plan.PlanDayText
                    },
                    new LocalizedProperty
                    {
                        Language = localizedItem.Language,
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