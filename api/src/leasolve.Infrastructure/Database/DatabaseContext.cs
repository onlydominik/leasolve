using leasolve.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace leasolve.Infrastructure.Database;

public sealed class DatabaseContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    private DatabaseContext()
    {
    }
    
    // If you need to operate on DbSet in application, add DbSet also in IApplicationDbContext
    
    public new DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AssemblyMarker).Assembly);
        
        base.OnModelCreating(builder);
        
        builder.Entity<User>().ToTable("Users", Schema.Identity);
        builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims", Schema.Identity);
        builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins", Schema.Identity);
        builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens", Schema.Identity);
        builder.Entity<IdentityRole<long>>().ToTable("Roles", Schema.Identity);
        builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims", Schema.Identity);
        builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles", Schema.Identity);
    }
}