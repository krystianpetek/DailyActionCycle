using DailyActionCycle.WebUI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyActionCycle.WebUI.Database.EntityConfiguration;

public class ActivityConfigurations : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Id)
            .IsRequired();

        builder.Property(prop => prop.Title)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(prop => prop.Description)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(prop => prop.CreatedAt)
            .IsRequired();

        builder.Property(prop => prop.DueDate)
            .IsRequired();

        builder.Property(prop => prop.IsCompleted)
            .IsRequired();

        builder.Property(prop => prop.IsNotified)
            .IsRequired();

        //builder.Property(habit => habit.Daily)
        //    .IsRequired();

        //builder.Property(habit => habit.Weekly)
        //    .IsRequired();

        builder.Property(prop => prop.UpdatedAt);

        builder.Property(prop => prop.IsDeleted);

        builder.Property(prop => prop.DeletedAt);

    }
}
