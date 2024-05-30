using DailyActionCycle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DailyActionCycle.WebAPI.Database;

public class DailyActionCycleDbContext(DbContextOptions<DailyActionCycleDbContext> options) : DbContext(options)
{
    public DbSet<Day> Days { get; set; }
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<ActionTemplate> ActionTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}