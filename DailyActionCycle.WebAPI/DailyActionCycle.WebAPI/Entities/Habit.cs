using DailyActionCycle.Core.Entities.Abstracts;

namespace DailyActionCycle.Core.Entities;
public record class Habit : Entity
{
    public required bool Daily { get; init; }

    public required bool Weekly { get; init; }
}
