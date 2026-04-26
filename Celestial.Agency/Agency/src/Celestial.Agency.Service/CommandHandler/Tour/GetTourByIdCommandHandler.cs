using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Geo;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.Agency.Service.Command.Tour;
using Celestial.SharedKernel.Common;
using Mapster;
using MediatR;

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
        // 🔹 پیدا کردن Slug
        var localizedProperty = await localizedPropertyService.FirstOrDefaultAsync(
            p => p.LocalKey == "Slug" &&
                 p.LocalValue == request.Slug &&
                 p.LocalKeyGroup == LocalKeyGroupType.Tour,
            cancellationToken: cancellationToken);

        if (localizedProperty == null)
            return new GetTourByIdCommandRes
            {
                Error = new ValidationError("T404", "Tour not found with provided slug")
            };

        // 🔹 دریافت اطلاعات تور
        var tour = await tourService.GetTourById(localizedProperty.EntityId, cancellationToken);
        if (tour == null)
            return new GetTourByIdCommandRes
            {
                Error = new ValidationError("T404", "Tour Not Found")
            };

        tour.Views++;

        // 🔹 تصویر اصلی تور
        var tourImage = await imageService.FirstOrDefaultAsync(
            p => p.EntityId == tour.Id,
            cancellationToken: cancellationToken);

        // 🔹 داده‌های لوکالایز
        var localizedData = await localizedPropertyService.GetPropertyByLanguageAndEntityId(
            new GetLocalizedPropertyReq
            {
                EntityId = tour.Id,
                Language = localizedProperty.Language,
                LocalKeyGroup = LocalKeyGroupType.Tour
            },
            cancellationToken);

        // 🔹 پر کردن DTO اصلی
        var itemResponse = tour.Adapt<GetTourItemRes>();
        itemResponse.DaysUntilRegistrationDeadline =
            tour.RegistrationDeadline.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber;

        // 🔹 کشورها
        //foreach (var country in tour.Countries)
        //{
        //    var countryData = await countryService.FindByIdAsync(cancellationToken, country);
        //    itemResponse.Countries.Add(countryData.Shortname);
        //}

        // 🔹 فیلدهای لوکالایز
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

        // 🔹 پلان‌ها
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
                continue;

            var planImages =
                await imageService.GetImagesByEntityId(tour.TourPlans[i].Id, "TourPlan", cancellationToken);
            var planPicturePaths = planImages.Select(p => p.PicturePath).ToList();

            itemResponse.TourPlans[i].PlanText =
                tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == "PlanText")?.LocalValue ?? string.Empty;
            itemResponse.TourPlans[i].PlanDayText =
                tourPlanLocalizeData.FirstOrDefault(p => p.LocalKey == "PlanDayText")?.LocalValue ?? string.Empty;
            itemResponse.TourPlans[i].PlanPicturePath = planPicturePaths;
        }

        // 🔹 تجهیزات ضروری
        itemResponse.NecessaryEquipment = new List<TourEquipmentDto>();
        if (tour.NecessaryEquipment?.Any() == true)
            foreach (var eqId in tour.NecessaryEquipment)
            {
                var eq = await equipmentsService.FindByIdAsync(cancellationToken, eqId);
                if (eq == null) continue;

                var localName = await localizedPropertyService.FirstOrDefaultAsync(
                    p => p.EntityId == eq.Id &&
                         p.LocalKeyGroup == LocalKeyGroupType.TourEquipment &&
                         p.LocalKey == "Name" &&
                         p.Language == localizedProperty.Language,
                    cancellationToken: cancellationToken);

                itemResponse.NecessaryEquipment.Add(new TourEquipmentDto
                {
                    Icon = eq.Icon,
                    Name = localName?.LocalValue ?? string.Empty
                });
            }

        // 🔹 تجهیزات غیرضروری
        itemResponse.UnNecessaryEquipment = new List<TourEquipmentDto>();
        if (tour.UnNecessaryEquipment?.Any() == true)
            foreach (var eqId in tour.UnNecessaryEquipment)
            {
                var eq = await equipmentsService.FindByIdAsync(cancellationToken, eqId);
                if (eq == null) continue;

                var localName = await localizedPropertyService.FirstOrDefaultAsync(
                    p => p.EntityId == eq.Id &&
                         p.LocalKeyGroup == LocalKeyGroupType.TourEquipment &&
                         p.LocalKey == "Name" &&
                         p.Language == localizedProperty.Language,
                    cancellationToken: cancellationToken);

                itemResponse.UnNecessaryEquipment.Add(new TourEquipmentDto
                {
                    Icon = eq.Icon,
                    Name = localName?.LocalValue ?? string.Empty
                });
            }

        await tourService.UnitOfWork.SaveChangesAsync(cancellationToken);

        // 🔹 سایر تورها برای نمایش در similar
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