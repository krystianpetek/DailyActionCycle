using System.Net.Http.Json;
using System.Text.Json;

namespace DailyActionCycle.WebUI.Services;

public class ActionTemplateService
{
    private readonly HttpClient _httpClient;
    private Dictionary<DateOnly, List<TaskItem>> _tasksByDate = new Dictionary<DateOnly, List<TaskItem>>();

    public ActionTemplateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TaskItem>> GetTasksForDate(DateTime date)
    {
        var dateOnly = DateOnly.FromDateTime(date);

        if (_tasksByDate.ContainsKey(dateOnly))
        {
            return _tasksByDate[dateOnly];
        }

        try
        {
            var response = await _httpClient.GetAsync($"/api/days/{date:yyyy-MM-dd}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var day = JsonSerializer.Deserialize<Day>(content, options);

                var taskList = day.Activities.Select(a => new TaskItem
                {
                    Date = day.Date,
                    Id = a.Id,
                    Name = a.Title,
                    Time = TimeOnly.FromDateTime(a.DueDate),
                    IsCompleted = a.IsCompleted,
                    HasNotification = a.IsNotified
                }).ToList();

                _tasksByDate[dateOnly] = taskList;
                return taskList;
            }

        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
        return new List<TaskItem>();
    }
    public async Task AddTask(TaskItem task)
    {
        var dateOnly = task.Date;

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/tasks", task);
            if (response.IsSuccessStatusCode)
            {
                var createdTask = await response.Content.ReadFromJsonAsync<TaskItem>();

                if (createdTask != null)
                {
                    if (_tasksByDate.ContainsKey(task.Date))
                    {
                        _tasksByDate[dateOnly].Add(createdTask);
                    }
                    else
                    {
                        _tasksByDate[dateOnly] = new List<TaskItem> { createdTask };
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to add the task. Server returned an error.");
            }
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Failed to add the task. Please check your internet connection.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public async Task UpdateTask(TaskItem task)
    {
        var dateOnly = task.Date;

        var response = await _httpClient.PutAsJsonAsync($"api/tasks/{task.Id}", task);
        if (response.IsSuccessStatusCode)
        {
            var updatedTask = await response.Content.ReadFromJsonAsync<TaskItem>();
            if (updatedTask != null)
            {
                if (_tasksByDate.ContainsKey(dateOnly))
                {
                    var index = _tasksByDate[dateOnly].FindIndex(t => t.Id == updatedTask.Id);
                    if (index != -1)
                    {
                        _tasksByDate[dateOnly][index] = updatedTask;
                    }
                }
            }
        }
    }
}

public sealed record class ActionTemplate
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; }

    public List<Activity> Activities { get; init; } = [];
}
