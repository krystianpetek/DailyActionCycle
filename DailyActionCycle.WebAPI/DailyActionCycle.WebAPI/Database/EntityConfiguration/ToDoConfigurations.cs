using DailyActionCycle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyActionCycle.WebAPI.Database.EntityConfiguration;

public class ToDoConfigurations : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
		builder.Property(x => x.DueDate).IsRequired();
    }
}
