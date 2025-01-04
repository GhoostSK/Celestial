using Celestial.ParvazBolourin.Api.Identity.Model;

namespace Celestial.ParvazBolourin.Api.Identity.Context
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

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
        }
    }
}
