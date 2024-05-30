namespace DailyActionCycle.Core.Entities;
public class ActionTemplate
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; }

    public List<ToDo> Tasks { get; init; } = [];

    public List<Habit> Habits { get; init; } = [];
}
