using Celestial.Agency.Api.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Celestial.Agency.Api.Identity.Context;

public class IdentityDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(entity => { entity.ToTable("IdentityUsers"); });
        builder.Entity<IdentityRole>(entity => { entity.ToTable("IdentityRoles"); });
        builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("IdentityUserRoles"); });
        builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("IdentityUserClaims"); });
        builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("IdentityUserLogins"); });
        builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("IdentityRoleClaims"); });
        builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("IdentityUserTokens"); });
        builder.Entity<User>(e => e.Ignore("Email"));
        builder.Entity<User>(e => e.Ignore("NormalizedEmail"));
        builder.Entity<User>(e => e.Ignore("EmailConfirmed"));
    }
}