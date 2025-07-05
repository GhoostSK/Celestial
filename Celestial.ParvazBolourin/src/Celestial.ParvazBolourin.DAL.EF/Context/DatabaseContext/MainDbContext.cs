using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;

public sealed class MainDbContext : AppDbContextBase
{
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Tours> Tours { get; set; }
    public DbSet<Language> Language { get; set; }
    public DbSet<TourPlans> TourPlans { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<LocalizedProperty> LocalizedProperties { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<City> City { get; set; }
}
