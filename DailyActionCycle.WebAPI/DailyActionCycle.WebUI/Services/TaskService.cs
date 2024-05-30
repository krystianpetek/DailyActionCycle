namespace DailyActionCycle.WebUI.Services;

public class TaskService
{
    private List<TaskItem> tasks = new List<TaskItem>()
    {
        new TaskItem
        {
            Date = DateTime.Now,
            Time = new TimeOnly(),
            HasNotification = true,
            IsCompleted = false,
            Name = "Task 1"
        }
    };

    public List<TaskItem> GetTasksForDate(DateTime date)
    {
        return tasks.Where(t => t.Date.Date == date.Date).ToList();
    }

    public void AddTask(TaskItem task)
    {
        tasks.Add(task);
    }

    public void RemoveTask(TaskItem task)
    {
        tasks.Remove(task);
    }

    public void SetNotification(TaskItem task)
    {
        task.HasNotification = true;
        // Implement notification logic here
    }

    public void UnsetNotification(TaskItem task)
    {
        task.HasNotification = false;
        // Implement notification removal logic here
    }
}

public class TaskItem
{
    public string Name { get; set; }
    public TimeOnly Time { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }
    public bool HasNotification { get; set; }
}