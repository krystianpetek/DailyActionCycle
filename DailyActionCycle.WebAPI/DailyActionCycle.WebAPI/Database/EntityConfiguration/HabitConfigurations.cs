using DailyActionCycle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyActionCycle.WebAPI.Database.EntityConfiguration;

public class HabitConfigurations : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.Property(habit => habit.Daily)
            .IsRequired();
        builder.Property(habit => habit.Weekly)
            .IsRequired();
    }
}
