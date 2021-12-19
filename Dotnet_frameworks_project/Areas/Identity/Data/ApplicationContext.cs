using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_frameworks_project.Areas.Identity.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Patient> Patient { get; set; }

    public DbSet<Dotnet_frameworks_project.Models.FollowUp_type> FollowUp_type { get; set; }

    public DbSet<Dotnet_frameworks_project.Models.FollowUp_patients> FollowUp_patients { get; set; }

    public DbSet<Dotnet_frameworks_project.Models.Test> Test { get; set; }

    public DbSet<Dotnet_frameworks_project.Models.PassedTests> PassedTests { get; set; }

    public DbSet<Dotnet_frameworks_project.Models.Insurance> Insurance { get; set; }
}
