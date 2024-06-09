using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Media;
using Android.OS;
using Android.Util;
using AndroidX.Core.App;
using DailyActionCycle.WebUI.Platforms.Android;

namespace DailyActionCycle.WebUI.Services;

public static class AppNotificationManager
{
    private static readonly int NotificationId = 1000;

    public static void ShowNotification(string title, string message)
    {
        var context = Android.App.Application.Context;
        var intent = new Intent(context, typeof(MainActivity)); // Replace MainActivity with your app's main activity
        intent.AddFlags(ActivityFlags.SingleTop);
        intent.AddFlags(ActivityFlags.ClearTop);

        var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent);

        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, "channel_id")
            .SetContentTitle(title)
            .SetContentText(message)
            .SetSmallIcon(Resource.Drawable.ic_call_answer) // Ensure you have an appropriate icon here
            .SetPriority(NotificationCompat.PriorityMax)
            .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
            .SetCategory(NotificationCompat.CategoryMessage)
            .SetContentTitle("title")
            .SetContentText("text")
            .SetVisibility(NotificationCompat.VisibilityPublic)
            .SetContentIntent(pendingIntent)
            .SetAutoCancel(true); // This removes the notification from the status bar once clicked

        // Optionally, use SetFullScreenIntent to display as a full-screen alert
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            builder.SetFullScreenIntent(pendingIntent, true);
        }

        NotificationChannel channel = new NotificationChannel("chantnel_id", "Notifications", NotificationImportance.High);
        channel.Description = "Notification channel for app alerts.";

        var notificationManager = context.GetSystemService(Context.NotificationService) as Android.App.NotificationManager;
        notificationManager.CreateNotificationChannel(channel);

        notificationManager.Notify(0, builder.Build());
    }

    public static void ScheduleNotification(string title, string message, int hour, int minute, int second)
    {
        var context = Android.App.Application.Context;
        var intent = new Intent(context, typeof(AlarmReceiver));
        intent.PutExtra("title", title);
        intent.PutExtra("message", message);

        var pendingIntent = PendingIntent.GetBroadcast(context, NotificationId, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

        var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

        // Initialize the calendar to now
        var alarmTime = Calendar.Instance;
        alarmTime.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();

        // Set the desired time
        alarmTime.Set(CalendarField.HourOfDay, hour);
        alarmTime.Set(CalendarField.Minute, minute);
        alarmTime.Set(CalendarField.Second, second);
        alarmTime.Set(CalendarField.Millisecond, 0);

        Log.Debug("AppNotificationManager", $"Scheduled time: {alarmTime.Time}");

        // If the alarm time has already passed for today, set it for tomorrow
        if (alarmTime.TimeInMillis <= Java.Lang.JavaSystem.CurrentTimeMillis())
        {
            alarmTime.Add(CalendarField.DayOfMonth, 1);
            Log.Debug("AppNotificationManager", $"Adjusted time to next day: {alarmTime.Time}");
        }

        // Use SetExactAndAllowWhileIdle for more precise timing
        alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, alarmTime.TimeInMillis, pendingIntent);
        Log.Debug("AppNotificationManager", $"Alarm set for: {alarmTime.Time}");
    }

}

[BroadcastReceiver(Enabled = true, Exported = true)]
public class AlarmReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        var title = intent.GetStringExtra("title");
        var message = intent.GetStringExtra("message");
        AppNotificationManager.ShowNotification(title, message);

        Log.Debug("AlarmReceiver", $"Notification shown: {title} - {message}");

    }
}
