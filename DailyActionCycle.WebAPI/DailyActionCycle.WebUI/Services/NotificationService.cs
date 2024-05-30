//[assembly: Dependency(typeof(NotificationService))]
namespace DailyActionCycle.WebUI.Services;

public class NotificationService : INotificationService
{
    public void ShowNotification(string title, string message)
    {
        AppNotificationManager.ShowNotification(title, message);
    }

    public void ScheduleNotification(string title, string message, TimeSpan time)
    {
        int hour = time.Hours;
        int minute = time.Minutes;
        int second = time.Seconds;
        AppNotificationManager.ScheduleNotification(title, message, hour, minute, second);
    }
}
