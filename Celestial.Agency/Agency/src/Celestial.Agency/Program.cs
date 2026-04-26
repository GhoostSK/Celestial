using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Celestial.Agency.Api.Identity.Context;
using Celestial.Agency.Api.Identity.Model;
using Celestial.Agency.Api.Identity.Services.JwtToken;
using Celestial.Agency.Api.Identity.Services.UserIdModelBinder;
using Celestial.Agency.Core;
using Celestial.Agency.DAL.EF.DependencyInjection;
using Celestial.Dal.Ef.SqlServer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseIISIntegration();
var hostEnvironment = builder.Environment;
builder.Services.AddHttpClient();
builder.Services.AddDalEf(builder.Configuration, hostEnvironment);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();

builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ActivityLogBehavior<,>));
//builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.RegisterServices();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new TokenUserIdModelBinderProvider());
});

//builder.WebHost.UseKestrel();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Your API Title",
        Version = "v1"
    });

    // JWT Auth config
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR...\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
});

builder.Services.AddAuthorization(option => { option.AddPolicy("Admin", policy => { policy.RequireRole("Admin"); }); });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/Account/Login";
    options.AccessDeniedPath = "/api/Account/AccessDenied";
    options.SlidingExpiration = true;
});
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await TourSeeder.SeedAsync(services);
//}

app.Run();