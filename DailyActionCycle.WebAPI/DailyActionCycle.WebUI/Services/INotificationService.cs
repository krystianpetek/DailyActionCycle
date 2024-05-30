namespace DailyActionCycle.WebUI.Services;
public interface INotificationService
{
    void ShowNotification(string title, string message);

    void ScheduleNotification(string title, string message, TimeSpan time);
}

