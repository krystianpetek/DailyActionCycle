﻿using DailyActionCycle.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyActionCycle.WebAPI.Database.EntityConfiguration;

public class ActionTemplateConfiguratrions : IEntityTypeConfiguration<ActionTemplate>
{
    public void Configure(EntityTypeBuilder<ActionTemplate> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Id)
            .IsRequired();

        builder.Property(prop => prop.Name)
            .IsRequired();

        builder.Property(prop => prop.Activities);

        builder.HasOne(prop => prop.Activities)
            .WithMany();
    }
}
