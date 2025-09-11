using System.Globalization;
using System.Text.RegularExpressions;
using Celestial.WebClient.Services.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var supportedCultures = new[] { "fa", "en", "ar", "ru" };

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

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
    var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();

    var routeCulture = segments.Count > 0 && supportedCultures.Contains(segments[0])
        ? segments[0]
        : null;

    var cookieCulture = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName]?
        .Split('|')
        .FirstOrDefault(c => c.StartsWith("c="))?
        .Substring(2);

    var finalCulture = routeCulture ?? cookieCulture ?? "fa";

    if (routeCulture == null)
    {
        var newSegments = new List<string> { finalCulture };
        newSegments.AddRange(segments);

        if (newSegments.Count > 2)
        {
            var slug = newSegments.Last();

            var isAlreadyEncoded = Regex.IsMatch(slug, "%[0-9A-Fa-f]{2}");
            if (!isAlreadyEncoded)
                slug = Uri.EscapeDataString(slug);

            newSegments[newSegments.Count - 1] = slug;
        }

        var safePath = "/" + string.Join("/", newSegments);

        context.Response.Redirect(safePath, false);
        return;
    }

    context.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(finalCulture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
    );

    var isRazorPage = !path.Contains("/api") &&
                      !path.Contains("/Language") &&
                      !path.Contains("/Home") &&
                      !path.Contains("/Controller");

    if (isRazorPage)
        context.Request.Path = "/" + string.Join("/", segments.Skip(1));

    await next();
});

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.MapRazorPages();

app.Run();