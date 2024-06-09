namespace DailyActionCycle.WebUI.Entities;

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

    public void AddActivity(Activity activity)
    {
        Activities.Add(activity);
    }

    public void RemoveActivity(Activity activity)
    {
        Activities.Remove(activity);
    }

    public void CompleteActivity(Activity activity)
    {
        Activities.Find(t => t.Id == activity.Id)?.Complete();
    }
}
