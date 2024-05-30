using DailyActionCycle.Core.Entities.Abstracts;

namespace DailyActionCycle.Core.Entities;
public record class ToDo : Entity
{
    public DateTime DueDate { get; init; }
    
    public bool Notify { get; init; }
}
