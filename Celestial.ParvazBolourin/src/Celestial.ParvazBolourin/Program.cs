using Castle.Core.Smtp;
using Celestial.ParvazBolourin.Api.Identity.Context;
using Celestial.ParvazBolourin.Api.Identity.Model;
using Celestial.ParvazBolourin.Core;
using Celestial.ParvazBolourin.DAL.EF.DependencyInjection;
using Celestial.ParvazBolourin.Service.Service.EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DependencyInjection;
using System.Reflection;
using Unleash;
//using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);
var hostEnvironment = builder.Environment;
builder.Services.AddHttpClient();
builder.Services.AddDalEf(builder.Configuration, hostEnvironment);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();

builder.Services.AddSingleton<IUnleash>(sp =>
{
    var unleashOptions = new UnleashSettings
    {
        AppName = "YourAppName",
        Environment = "production",
        UnleashApi = new Uri("https://prod-unleash-Celestial.Celestial.app/api"),
        InstanceTag = "instance-1",
        FetchTogglesInterval = TimeSpan.FromSeconds(10)
    };

    return new DefaultUnleash(unleashOptions);
});

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.RegisterServices();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
});

builder.Services.AddAuthorization(option => { option.AddPolicy("Admin", policy => { policy.RequireRole("Admin"); }); });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/Account/Login";
    options.AccessDeniedPath = "/api/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();