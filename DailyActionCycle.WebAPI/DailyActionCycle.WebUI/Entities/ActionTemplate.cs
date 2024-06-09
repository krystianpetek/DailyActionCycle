namespace DailyActionCycle.WebUI.Entities;
public sealed record class ActionTemplate
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; }

    public List<Activity> Activities { get; init; } = [];
}
