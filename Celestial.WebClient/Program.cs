using System.Globalization;
using Celestial.WebClient.Services.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var supportedCultures = new[] { "fa", "en", "ar", "ru" };

// اضافه کردن سرویس لوکالایزیشن
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// اضافه کردن لوکالایزیشن به Razor
builder.Services
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddRazorPages()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();

    options.DefaultRequestCulture = new RequestCulture("fa");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;

    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new RouteDataRequestCultureProvider
        {
            Options = options,
            RouteDataStringKey = "culture",
            UIRouteDataStringKey = "culture"
        },
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

builder.Services.AddWebClientServices(builder.Configuration);

var app = builder.Build();

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapControllerRoute(
    "localizedDefault",
    "{culture}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");


app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? "/";
    var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

    var routeCulture = segments.Length > 0 && supportedCultures.Contains(segments[0])
        ? segments[0]
        : null;

    var cookieCulture = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName]?
        .Split('|')
        .FirstOrDefault(c => c.StartsWith("c="))?
        .Substring(2);

    var finalCulture = routeCulture ?? cookieCulture ?? "fa";

    // اگر culture در URL نیست، ریدایرکت کن به مسیر با culture
    if (routeCulture == null)
    {
        var newPath = "/" + finalCulture + path;
        context.Response.Redirect(newPath);
        return;
    }

    // تنظیم کوکی
    context.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(finalCulture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
    );

    // فقط اگر مسیر مربوط به Razor Page باشه، مسیر رو rewrite کن
    var isRazorPage = !path.Contains("/api") && !path.Contains("/Language") && !path.Contains("/Home") &&
                      !path.Contains("/Controller");

    if (isRazorPage) context.Request.Path = "/" + string.Join("/", segments.Skip(1));

    await next();
});

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.MapRazorPages();

app.Run();