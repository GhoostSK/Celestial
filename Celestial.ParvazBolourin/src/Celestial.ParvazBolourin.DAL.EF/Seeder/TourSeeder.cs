using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.Enum.Global;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Celestial.ParvazBolourin.DAL.EF.Seeder;

public static class TourSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        T? GetRandomEnumValue<T>(Random random) where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length))!;
        }
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MainDbContext>();

        if (await db.Tours.AnyAsync()) return;

        var random = new Random();
        var languages = await db.Language.ToListAsync();
    
        for (int i = 0; i < 30; i++)
        {
            var countries = await db.Country.ToListAsync();

            var selectedCountries = countries.OrderBy(_ => random.Next()).Take(2).Select(c => c.Id).ToList();

            var tourStatus = GetRandomEnumValue<TourStatus>(random);
            var tour = new Tours
            {
                TourCode = $"TOUR-{i + 1:D4}",
                TourStatus = tourStatus,
                PassengerCapacity = (tourStatus == TourStatus.Planning) ? 0 : random.Next(10, 50),
                StartDate = (tourStatus == TourStatus.Planning) ? DateOnly.MinValue : DateOnly.FromDateTime(DateTime.Now.AddDays(random.Next(10, 100))),
                RegistrationDeadline = (tourStatus == TourStatus.Planning) ? DateOnly.MinValue : DateOnly.FromDateTime(DateTime.Now.AddDays(random.Next(1, 9))),
                TourUserRegistrations = 0,
                BasePrice = (tourStatus == TourStatus.Planning) ? 0 : random.Next(500, 1500),
                ForeignPrice = (tourStatus == TourStatus.Planning) ? 0 : random.Next(600, 2000),
                ForeignCurrency = (tourStatus == TourStatus.Planning) ? null : "USD",
                Score = random.Next(1, 10),
                TourContinent = GetRandomEnumValue<Continent>(random),
                TourType = GetRandomEnumValue<TourType>(random),
                TourScope = GetRandomEnumValue<TourScope>(random),
                RequiresVisa = true,
                ExecDate = Enumerable.Range(0, random.Next(1, 15))
            .Select(_ => DateOnly.FromDateTime(DateTime.Now.AddDays(random.Next(120))))
            .OrderBy(d => d)
            .Aggregate(new List<DateOnly>(), (list, date) =>
            {
                list.Add(list.Count == 0 ? date : list.Last().AddDays(random.Next(1, 3)));
                return list;
            }),
                TotalDay = random.Next(3, 10),
                Views = random.Next(1000),
                IsDeleted = false,
                PrimaryTransport = new TourPrimaryTransport
                {
                    LeadFlightCode = "IR001",
                    ReturnFlightCode = "IR002",
                    Date = DateTime.Now.AddDays(10),
                    LeadStartTime = "08:00",
                    LeadEndTime = "12:00",
                    ReturnStartTime = "14:00",
                    ReturnEndTimeTime = "18:00",
                    AirLine = "Iran Air",
                    AirLineImagePath = "Images/AirLine/Airline.jpg"
                },
                TransPorts = new List<TourTransPorts>
        {
            new()
            {
                TransPortType = GetRandomEnumValue<TransportVehicleType>(random),
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(2)
            }
        },
                NecessaryEquipment = [1],
                UnNecessaryEquipment = [2],
                CountriesId = selectedCountries,
                Hotels = [],
                TourPlans = new List<TourPlans>()
            };

            await db.Tours.AddAsync(tour);
            await db.SaveChangesAsync();

            int planCount = random.Next(3, 6);
            for (int p = 0; p < planCount; p++)
            {
                var plan = new TourPlans
                {
                    TourId = tour.Id,
                    PlanPictureId = new List<long>()
                };

                var planImages = new List<Images>();
                int imgCount = random.Next(1, 4);
                for (int img = 0; img < imgCount; img++)
                {
                    var image = new Images
                    {
                        PicturePath = "Images/tour/TourPlan.jpg",
                        DisplayOrder = img,
                        EntityId = 0,
                        KeyGroup = "TourPlan"
                    };
                    planImages.Add(image);
                }

                await db.Images.AddRangeAsync(planImages);
                await db.SaveChangesAsync();

                await db.TourPlans.AddAsync(plan);
                await db.SaveChangesAsync();

                foreach (var img in planImages)
                {
                    img.EntityId = plan.Id;
                }
                await db.SaveChangesAsync();

                plan.PlanPictureId = planImages.Select(i => i.Id).ToList();
                db.TourPlans.Update(plan);
                await db.SaveChangesAsync();
                foreach (var lang in languages)
                {
                    var planLocalizedProperties = new List<LocalizedProperty>
                    {
                        new LocalizedProperty
                        {
                            LanguageId = lang.Id,
                            LocalKeyGroup = LocalKeyGroupType.TourPlan,
                            EntityId = plan.Id,
                            LocalKey = "PlanDayText",
                            LocalValue = $"متن روز برنامه {p + 1} به زبان {lang.Name}"
                        },
                        new LocalizedProperty
                        {
                            LanguageId = lang.Id,
                            LocalKeyGroup = LocalKeyGroupType.TourPlan,
                            EntityId = plan.Id,
                            LocalKey = "PlanText",
                            LocalValue = $"متن برنامه {p + 1} به زبان {lang.Name}"
                        }
                    };

                    db.LocalizedProperties.AddRange(planLocalizedProperties);
                }


                tour.TourPlans.Add(plan);
            }

            var tourImage = new Images
            {
                EntityId = tour.Id,
                KeyGroup = "Tour",
                PicturePath = "Images/tour/Tour.jpg",
                DisplayOrder = 0
            };
            await db.Images.AddAsync(tourImage);


            foreach (var lang in languages)
            {
                var localizedProperties = new List<LocalizedProperty>
            {

                new()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "Name",
                    LocalValue = $"تور شماره {i + 1} ({lang.Name})"
                },
                new()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "TourMainText",
                    LocalValue = $"متن اصلی تور {i + 1} برای زبان {lang.Name}"
                },
                new()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "TourTips",
                    LocalValue = $"نکات مهم تور {i + 1} ({lang.Name})"
                },
                new()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "MetaKeywords",
                    LocalValue = $"تور {i + 1}, سفر, {lang.Name}"
                },
                new ()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "PlanShortText",
                    LocalValue = $"خلاصه برنامه تور {i + 1} - زبان {lang.Name}"
                },
                new()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "MetaTitle",
                    LocalValue = $"عنوان متای تور {i + 1} ({lang.Name})"
                },
                new()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "Slug",
                    LocalValue = $"tour-{i + 1}-{lang.Name.ToLower()}"
                },
                new()
                {
                    LanguageId = lang.Id,
                    LocalKeyGroup = LocalKeyGroupType.Tour,
                    EntityId = tour.Id,
                    LocalKey = "MetaDescription",
                    LocalValue = $"توضیحات متای تور {i + 1} به زبان {lang.Name}"
                }
            };
                db.LocalizedProperties.AddRange(localizedProperties);
            }

            await db.SaveChangesAsync();
        }
    }

}






