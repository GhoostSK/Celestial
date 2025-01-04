using Microsoft.EntityFrameworkCore;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;

public sealed class MainDbContext : AppDbContextBase
{
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }
}