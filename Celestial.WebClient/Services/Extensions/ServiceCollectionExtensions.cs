using Celestial.WebClient.Services.ApiClients.Account;
using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.ApiClients.Blog;
using Celestial.WebClient.Services.ApiClients.Booking;
using Celestial.WebClient.Services.ApiClients.Comment;
using Celestial.WebClient.Services.ApiClients.Contact;
using Celestial.WebClient.Services.ApiClients.Currency;
using Celestial.WebClient.Services.ApiClients.Faq;
using Celestial.WebClient.Services.ApiClients.Geo;
using Celestial.WebClient.Services.ApiClients.Hotel;
using Celestial.WebClient.Services.ApiClients.Newsletter;
using Celestial.WebClient.Services.ApiClients.Tour;
using Celestial.WebClient.Services.ApiClients.Visa;
using Celestial.WebClient.Services.Authentication;
using Celestial.WebClient.Services.Implementation;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebClientServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.AddMemoryCache();
        services.AddScoped<IUserSessionService, UserSessionService>();

        services.AddHttpClient<HttpJsonClient>(client => { client.BaseAddress = new Uri(config["ApiBaseUrl"]!); });

        // API clients
        services.AddScoped<AccountApiClient>();
        services.AddScoped<BlogApiClient>();
        services.AddScoped<BookingApiClient>();
        services.AddScoped<CommentApiClient>();
        services.AddScoped<ContactUsApiClient>();
        services.AddScoped<CurrencyApiClient>();
        services.AddScoped<FaqApiClient>();
        services.AddScoped<GeoApiClient>();
        services.AddScoped<HotelApiClient>();
        services.AddScoped<NewsletterApiClient>();
        services.AddScoped<TourApiClient>();
        services.AddScoped<VisaApiClient>();

        // Application services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IContactUsService, ContactUsService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IFaqService, FaqService>();
        services.AddScoped<IGeoService, GeoService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<INewsletterService, NewsletterService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IVisaService, VisaService>();

        return services;
    }
}

public class ServiceResContextBase
{
    public bool HasError { get; set; } = false;
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
