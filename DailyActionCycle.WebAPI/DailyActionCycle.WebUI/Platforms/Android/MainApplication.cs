using Android.App;
using Android.Runtime;
using DailyActionCycle.WebUI.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace DailyActionCycle.WebUI.Platforms.Android;
[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(nint handle, JniHandleOwnership ownership)
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
        builder.Services.AddSingleton<HttpClientUrlService>();

        var httpClientHandler = new Xamarin.Android.Net.AndroidMessageHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = (a, b, c, d) => true;

        builder.Services.AddHttpClient("TaskService").ConfigurePrimaryHttpMessageHandler(mh => httpClientHandler);
        builder.Services.AddSingleton<TaskService>();
        builder.Services.AddSingleton<ActionTemplateService>();

        //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //string databasePath = Path.Combine(folderPath, "DailyActionCycle.db");
        //builder.Services.AddDbContext<DailyActionCycleDbContext>(options =>
        //{
        //    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "yourdatabase.db");
        //    options.UseSqlite($"Filename={dbPath}");
        //});

        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(MainApplication).Assembly);
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
