using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Base;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.DAL.EF.Context.DatabaseContext;

public sealed class MainDbContext : AppDbContextBase, IUnitOfWorkEf
{
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    //public DbSet<Tours> Tours { get; set; }
    //public DbSet<Language> Language { get; set; }
    //public DbSet<TourPlans> TourPlans { get; set; }
    //public DbSet<Images> Images { get; set; }
    //public DbSet<LocalizedProperty> LocalizedProperties { get; set; }
    //public DbSet<Country> Country { get; set; }
    //public DbSet<City> City { get; set; }
}