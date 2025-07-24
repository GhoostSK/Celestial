using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Core.Enum.Tour;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.Agency.Service.Command.Tour;
using Mapster;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.Tour;

public class GetTourByIdCommandHandler(
    ITourService tourService,
    ILocalizedPropertyService localizedPropertyService,
    IImageService imageService,
    ICountryService countryService,
    ITourEquipmentsService equipmentsService,
    IMediator mediator)
    : IRequestHandler<GetTourCommandReq, GetTourByIdCommandRes>
{
    public async Task<GetTourByIdCommandRes> Handle(GetTourCommandReq request, CancellationToken cancellationToken)
    {
        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(p =>
            p.LocalKey == "Slug" &&
            p.LocalValue == request.Slug &&
            p.LocalKeyGroup == LocalKeyGroupType.Tour, cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetTourByIdCommandRes
            {
                Error = new ValidationError("T404", "Tour not found with provided slug")
            };

        var tour = await tourService.GetTourById(localizedProperty.EntityId, cancellationToken);

        if (tour == null)
            return new GetTourByIdCommandRes
            {
                Error = new ValidationError("T404", "Tour Not Found")
            };

        tour.Views++;
        var tourImage =
            await imageService.FirstOrDefaultAsync(p => p.EntityId == tour.Id, cancellationToken: cancellationToken);
        var localizedData = await localizedPropertyService.GetPropertyByLanguageAndEntityId(new GetLocalizedPropertyReq
        {
            EntityId = tour.Id,
            Language = localizedProperty.Language,
            LocalKeyGroup = LocalKeyGroupType.Tour
        }, cancellationToken);

        var itemResponse = tour.Adapt<GetTourItemRes>();
        itemResponse.DaysUntilRegistrationDeadline =
            tour.RegistrationDeadline.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber;
        foreach (var country in tour.CountriesId)
        {
            var countryData = await countryService.FindByIdAsync(cancellationToken, country);
            itemResponse.Countries.Add(countryData.Shortname);
        }

        itemResponse.PicturePath = tourImage?.PicturePath ?? string.Empty;
        itemResponse.Name = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.Name))?.LocalValue ??
                            string.Empty;
        itemResponse.TourMainText =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.TourMainText))?.LocalValue ??
            string.Empty;
        itemResponse.TourTips =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.TourTips))?.LocalValue ??
            string.Empty;
        itemResponse.PlanShortText =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.PlanShortText))?.LocalValue ??
            string.Empty;
        itemResponse.TourFacilities = localizedData.Where(p => p.LocalKey == nameof(GetTourItemRes.TourFacilities))
            .Select(p => p.LocalValue).ToList();
        itemResponse.Slug = localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.Slug))?.LocalValue ??
                            string.Empty;
        itemResponse.MetaDescription =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.MetaDescription))?.LocalValue ??
            string.Empty;
        itemResponse.MetaKeywords =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.MetaKeywords))?.LocalValue ??
            string.Empty;
        itemResponse.MetaTitle =
            localizedData.FirstOrDefault(p => p.LocalKey == nameof(GetTourItemRes.MetaTitle))?.LocalValue ??
            string.Empty;

        for (var i = 0; i < tour.TourPlans.Count; i++)
        {
            var tourPlanLocalizeData = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = tour.TourPlans[i].Id,
                    Language = localizedProperty.Language,
                    LocalKeyGroup = LocalKeyGroupType.TourPlan
                }, cancellationToken);

            if (!tourPlanLocalizeData.Any())
                break;

            var planImages =
                await imageService.GetImagesByEntityId(tour.TourPlans[i].Id, "TourPlan", cancellationToken);
            var planPicturePaths = planImages.Select(p => p.PicturePath).ToList();

            itemResponse.TourPlans[i].PlanText =
                tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == "PlanText")?.LocalValue ?? string.Empty;
            itemResponse.TourPlans[i].PlanDayText =
                tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == "PlanDayText")?.LocalValue ?? string.Empty;
            itemResponse.TourPlans[i].PlanPicturePath = planPicturePaths;
        }

        itemResponse.NecessaryEquipment = new List<TourEquipmentDto>();
        itemResponse.UnNecessaryEquipment = new List<TourEquipmentDto>();
        foreach (var item in tour.NecessaryEquipment)
        {
            var equipment = await equipmentsService.FindByIdAsync(cancellationToken, item);
            var localizeName = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.EntityId == item && p.LocalKeyGroup == LocalKeyGroupType.TourEquipment && p.LocalKey == "Name" &&
                p.Language == localizedProperty.Language, cancellationToken: cancellationToken);
            itemResponse.NecessaryEquipment.Add(new TourEquipmentDto
            {
                Icon = equipment?.Icon ?? "",
                Name = localizeName?.LocalValue ?? ""
            });
        }

        foreach (var item in tour.UnNecessaryEquipment)
        {
            var equipment = await equipmentsService.FindByIdAsync(cancellationToken, item);
            var localizeName = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.EntityId == item && p.LocalKeyGroup == LocalKeyGroupType.TourEquipment && p.LocalKey == "Name" &&
                p.Language == localizedProperty.Language, cancellationToken: cancellationToken);
            itemResponse.UnNecessaryEquipment.Add(new TourEquipmentDto
            {
                Icon = equipment?.Icon ?? "",
                Name = localizeName?.LocalValue ?? ""
            });
        }

        await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        var tourData = await tourService.GetToursList(new GetTourListReq
        {
            PageSize = 6,
            PageNumber = 1
        }, cancellationToken);

        var similarTours = new List<GetToursListItemRes>();

        foreach (var item in tourData.Items)
        {
            var localizeData = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    EntityId = item.Id,
                    Language = localizedProperty.Language,
                    LocalKeyGroup = LocalKeyGroupType.Tour
                }, cancellationToken);

            var name = localizeData.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Name))
                ?.LocalValue;
            var tourText = localizeData
                .FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.TourMainText))?.LocalValue;
            var slug = localizeData.FirstOrDefault(p => p.LocalKey == nameof(TourLocalizedPropertyItem.Slug))
                ?.LocalValue;
            var picture = await imageService.FirstOrDefaultAsync(
                p => p.EntityId == item.Id && p.KeyGroup == "Tour" && p.DisplayOrder == 0,
                cancellationToken: cancellationToken);

            var countries = new List<string>();
            foreach (var country in item.CountriesId)
            {
                var countryData = await countryService.FindByIdAsync(cancellationToken, country);
                countries.Add(countryData.Shortname);
            }

            similarTours.Add(new GetToursListItemRes
            {
                Id = item.Id,
                Score = item.Score,
                Name = name!,
                StartDate = item.StartDate,
                TourText = tourText!,
                PicturePath = picture.PicturePath,
                Country = countries,
                TourStatus = item.TourStatus,
                Slug = slug!,
                DaysUntilRegistrationDeadline = item.TourStatus == TourStatus.Planning
                    ? null
                    : item.RegistrationDeadline.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber,
                RegistrationDeadline = item.TourStatus == TourStatus.Planning
                    ? null
                    : item.RegistrationDeadline,
                ForeignCurrency = item.ForeignCurrency,
                ForeignPrice = item.ForeignPrice,
                BasePrice = item.BasePrice,
                TotalDay = item.TotalDay
            });
        }

        var similarTour = await mediator.Send(new GetTourFeaturedCommandReq
        {
            Language = localizedProperty.Language,
            TopCount = 7
        }, cancellationToken);

        var similarBlog = await mediator.Send(new GetTopBlogsCommandReq
        {
            Language = localizedProperty.Language,
            TopCount = 3
        }, cancellationToken);

        itemResponse.SimilarBlog = similarBlog.Items;
        itemResponse.SimilarTour = similarTour.Items;
        return new GetTourByIdCommandRes
        {
            Item = itemResponse
        };
    }
}