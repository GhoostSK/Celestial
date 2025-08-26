//using Celestial.Agency.Service.Command.ShirdalBlog;
//using Celestial.Agency.Service.Command.ShirdalBlog.BlogCategory;
//using Celestial.Agency.Service.Command.ShirdalBlog.BlogTag;
//using MediatR;
//using Microsoft.Extensions.DependencyInjection;

//namespace Celestial.Agency.Service.Seeder;

//public static class BlogSeeder
//{
//    public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
//    {
//        var mediator = services.GetRequiredService<IMediator>();

//        // ---------- Categories ----------
//        var categories = new[]
//        {
//            new { Fa = "تکنولوژی", En = "Technology", Ar = "تكنولوجيا", Ru = "Технологии" },
//            new { Fa = "سفر", En = "Travel", Ar = "سفر", Ru = "Путешествия" },
//            new { Fa = "علم", En = "Science", Ar = "علم", Ru = "Наука" },
//            new { Fa = "فرهنگ", En = "Culture", Ar = "ثقافة", Ru = "Культура" },
//            new { Fa = "کسب‌وکار", En = "Business", Ar = "أعمال", Ru = "Бизнес" }
//        };

//        var categoryIds = new List<long>();
//        foreach (var cat in categories)
//        {
//            var res = await mediator.Send(new AddBlogCategoryCommandReq
//            {
//                LocalizeProperty = new List<LocalizedNameDto>
//                {
//                    new() { Language = "fa", Name = cat.Fa, Slug = cat.Fa.Replace(" ", "-") },
//                    new() { Language = "en", Name = cat.En, Slug = cat.En.ToLower().Replace(" ", "-") },
//                    new() { Language = "ar", Name = cat.Ar, Slug = cat.Ar.Replace(" ", "-") },
//                    new() { Language = "ru", Name = cat.Ru, Slug = cat.Ru.Replace(" ", "-") }
//                }
//            }, cancellationToken);
//        }

//        // ---------- Tags ----------
//        var tags = new[]
//        {
//            new { Fa = "هوش مصنوعی", En = "AI", Ar = "الذكاء الاصطناعي", Ru = "ИИ" },
//            new { Fa = "ابر", En = "Cloud", Ar = "سحابة", Ru = "Облако" },
//            new { Fa = "بلاکچین", En = "Blockchain", Ar = "بلوك تشين", Ru = "Блокчейн" },
//            new { Fa = "سفرنامه", En = "TravelTips", Ar = "نصائح السفر", Ru = "Советы путешественникам" },
//            new { Fa = "نجوم", En = "Astronomy", Ar = "علم الفلك", Ru = "Астрономия" },
//            new { Fa = "فرهنگ", En = "CultureShock", Ar = "صدمة ثقافية", Ru = "Культурный шок" },
//            new { Fa = "برنامه‌نویسی", En = "Programming", Ar = "برمجة", Ru = "Программирование" },
//            new { Fa = "طراحی", En = "Design", Ar = "تصميم", Ru = "Дизайн" }
//        };

//        var tagIds = new List<long>();
//        foreach (var tag in tags)
//        {
//            var res = await mediator.Send(new AddBlogTagCommandReq
//            {
//                LocalizedNames = new List<LocalizedTagNameDto>
//                {
//                    new() { Language = "fa", Name = tag.Fa, Slug = tag.Fa.Replace(" ", "-") },
//                    new() { Language = "en", Name = tag.En, Slug = tag.En.ToLower().Replace(" ", "-") },
//                    new() { Language = "ar", Name = tag.Ar, Slug = tag.Ar.Replace(" ", "-") },
//                    new() { Language = "ru", Name = tag.Ru, Slug = tag.Ru.Replace(" ", "-") }
//                }
//            }, cancellationToken);
//        }

//        // ---------- Blogs ----------
//        var rnd = new Random();

//        for (var i = 1; i <= 20; i++)
//        {
//            var randomCategories = categoryIds.OrderBy(_ => rnd.Next()).Take(rnd.Next(1, 3)).ToList();
//            var randomTags = tagIds.OrderBy(_ => rnd.Next()).Take(rnd.Next(2, 5)).ToList();

//            await mediator.Send(new AddBlogCommandReq
//            {
//                PicturePath = $"https://source.unsplash.com/random/800x600?sig={i}&blog",
//                BlogCategoryIds = randomCategories,
//                BlogTagIds = randomTags,
//                BlogLocalizedProperties = new List<BlogLocalizedProperty>
//                {
//                    new()
//                    {
//                        Language = "fa",
//                        Title = $"وبلاگ شماره {i}",
//                        HeaderText = $"هدر وبلاگ {i}",
//                        MainText = $"محتوای فارسی برای وبلاگ {i}",
//                        MetaTitle = $"عنوان متا {i}",
//                        MetaDescription = $"توضیحات متا {i}",
//                        MetaKeywords = $"کلمات کلیدی {i}",
//                        Slug = $"وبلاگ-{i}"
//                    },
//                    new()
//                    {
//                        Language = "en",
//                        Title = $"Blog Post {i}",
//                        HeaderText = $"Header {i}",
//                        MainText = $"English content for blog {i}",
//                        MetaTitle = $"Meta {i}",
//                        MetaDescription = $"Meta description {i}",
//                        MetaKeywords = $"keywords {i}",
//                        Slug = $"blog-{i}"
//                    },
//                    new()
//                    {
//                        Language = "ar",
//                        Title = $"مقالة {i}",
//                        HeaderText = $"رأس {i}",
//                        MainText = $"المحتوى العربي للمقالة {i}",
//                        MetaTitle = $"ميتا {i}",
//                        MetaDescription = $"وصف ميتا {i}",
//                        MetaKeywords = $"كلمات {i}",
//                        Slug = $"مقالة-{i}"
//                    },
//                    new()
//                    {
//                        Language = "ru",
//                        Title = $"Блог {i}",
//                        HeaderText = $"Заголовок {i}",
//                        MainText = $"Русское содержание блога {i}",
//                        MetaTitle = $"Мета {i}",
//                        MetaDescription = $"Описание {i}",
//                        MetaKeywords = $"ключевые {i}",
//                        Slug = $"блог-{i}"
//                    }
//                }
//            }, cancellationToken);
//        }
//    }
//}

