namespace DailyActionCycle.WebAPI.Entities;

public sealed record class Day
{
    public Day(DateOnly date)
    {
        Id = Guid.NewGuid();
        Date = date;
    }

    public Guid Id { get; init; }

    public DateOnly Date { get; init; }

    public Guid? ActionTemplateId { get; set; }
    public ActionTemplate? ActionTemplate { get; set; }

    public List<Activity> Activities { get; set; } = [];


    public IEnumerable<Activity> IncompletedActivities() => Activities
        .Where(activity => !activity.IsCompleted)
        .AsEnumerable();

    public IEnumerable<Activity> CompletedActivities() => Activities
        .Where(activity => activity.IsCompleted)
        .AsEnumerable();

    public void AddActivity(Activity task)
    {
        Activities.Add(task);
    }

    public void RemoveActivity(Activity task)
    {
        Activities.Remove(task);
    }

    public void CompleteActivity(Activity task)
    {
        Activities.Find(t => t.Id == task.Id)?.Complete();
    }
}
