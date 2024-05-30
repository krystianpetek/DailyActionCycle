using DailyActionCycle.Core.Entities;
using DailyActionCycle.Core.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyActionCycle.WebAPI.Database.EntityConfiguration;

public class EntityConfigurations : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.HasKey(habit => habit.Id);
        builder.Property(habit => habit.Description)
            .IsRequired();
        builder.Property(habit => habit.Name)
            .IsRequired();

        builder.Property(x => x.Completed).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);
    }
}
