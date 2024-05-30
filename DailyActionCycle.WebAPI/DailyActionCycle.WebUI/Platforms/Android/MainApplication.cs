using Android.App;
using Android.Runtime;
using DailyActionCycle.WebUI.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace DailyActionCycle.WebUI;
[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp()
    {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

            builder.Services.AddSingleton<INotificationService, NotificationService>();
            builder.Services.AddScoped<CurrentRouteService>();
        builder.Services.AddSingleton<TaskService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
