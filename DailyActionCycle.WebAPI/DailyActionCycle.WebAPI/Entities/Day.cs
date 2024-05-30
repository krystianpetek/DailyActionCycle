using DailyActionCycle.Core.Entities.Abstracts;

namespace DailyActionCycle.Core.Entities;

public class Day
{
    public Day(DateOnly date)
    {
        Id = Guid.NewGuid();
        Date = date;
    }

    public Guid Id { get; init; }

    public DateOnly Date { get; init; }

    public ActionTemplate? ActionTemplate { get; set; }

    public List<Entity> Tasks { get; set; } = [];

    public List<Entity> Habits { get; set; } = [];

    public IEnumerable<Entity> IncompletedActivities() => Tasks
        .Union(Habits)
        .Where(activity => !activity.Completed)
        .AsEnumerable();

    public IEnumerable<Entity> CompletedActivities() => Tasks
        .Union(Habits)
        .Where(activity => activity.Completed)
        .AsEnumerable();

    public void AddActivity(Entity activity)
    {
        if (activity is ToDo task)
        {
            Tasks.Add(task);
        }
        else if (activity is Habit habit)
        {
            Habits.Add(habit);
        }
    }

    public void RemoveActivity(Entity activity)
    {
        if (activity is ToDo task)
        {
            Tasks.Remove(task);
        }
        else if (activity is Habit habit)
        {
            Habits.Remove(habit);
        }
    }

    public void CompleteActivity(Entity activity)
    {
        if (activity is ToDo task)
        {
            Tasks.Find(t => t.Id == task.Id)?.Complete();
        }
        else if (activity is Habit habit)
        {
            Habits.Find(t => t.Id == habit.Id)?.Complete();
        }
    }
}
