using DailyActionCycle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyActionCycle.WebAPI.Database.EntityConfiguration;

public class DayConfigurations : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasKey(day => day.Date);
        builder.Property(day => day.Date)
            .HasConversion(
                           date => date.ToString(),
                                          date => DateOnly.Parse(date)
                                                     );

        builder.HasMany(day => day.Tasks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(day => day.Habits)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
