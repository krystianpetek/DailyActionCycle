using System.Net.Http.Json;
using System.Text.Json;

namespace DailyActionCycle.WebUI.Services;

public class TaskService
{
    private HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClientUrlService _httpClientUrlService;
    private Dictionary<DateOnly, List<TaskItem>> _tasksByDate = new Dictionary<DateOnly, List<TaskItem>>();

    public TaskService(IHttpClientFactory httpClient, HttpClientUrlService httpClientUrlService)
    {
        _httpClientFactory = httpClient;
        _httpClient = httpClient.CreateClient("TaskService");
        _httpClient.BaseAddress = new Uri(httpClientUrlService.Url);

        _httpClientUrlService = httpClientUrlService;
    }

    public bool ChangeHttpClientUri(string httpClientUri)
    {
        _httpClientUrlService.Url = httpClientUri;

        _httpClient = _httpClientFactory.CreateClient("TaskService");
        _httpClient.BaseAddress = new Uri(_httpClientUrlService.Url);


        return true;
    }

    public string GetHttpClientUri() => _httpClientUrlService.Url;

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

    //public void AddTask(TaskItem task)
    //{
    //    tasks.Add(task);
    //}

    //public void RemoveTask(TaskItem task)
    //{
    //    tasks.Remove(task);
    //}

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
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeOnly Time { get; set; }
    public DateOnly Date { get; set; }
    public bool IsCompleted { get; set; }
    public bool HasNotification { get; set; }
}

public class Day
{
    public Guid Id { get; init; }

    public DateOnly Date { get; init; }

    public Guid? ActionTemplateId { get; set; }
    public ActionTemplate? ActionTemplate { get; set; }

    public List<Activity> Activities { get; set; } = [];
}

public sealed record class Activity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Title { get; set; }

    public required string Description { get; set; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime DueDate { get; set; } = DateTime.UtcNow.AddHours(1);

    public bool IsCompleted { get; set; } = false;

    public bool IsNotified { get; set; } = true;


    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }


    public void Complete() => IsCompleted = true;
}
