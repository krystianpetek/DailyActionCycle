using DailyActionCycle.WebUI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace DailyActionCycle.WebUI.Database;

public class DailyActionCycleDbContext(DbContextOptions<DailyActionCycleDbContext> options) : DbContext(options)
{
    public DbSet<Day> Days { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActionTemplate> ActionTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DailyActionCycleDbContext>
{
    public DailyActionCycleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DailyActionCycleDbContext>();
        optionsBuilder.UseSqlite("Filename=yourdatabase.db");

        return new DailyActionCycleDbContext(optionsBuilder.Options);
    }
}