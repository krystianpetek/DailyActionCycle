using DailyActionCycle.WebUI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyActionCycle.WebUI.Database.EntityConfiguration;

public class DayConfigurations : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasKey(prop => prop.Date);

        builder.Property(prop => prop.Id)
            .IsRequired();

        builder.Property(prop => prop.Date)
            .HasConversion(date => date.ToString(), date => DateOnly.Parse(date));

        builder.HasMany(prop => prop.Activities)
            .WithMany();

        builder.HasOne(prop => prop.ActionTemplate)
            .WithMany();
    }
}
