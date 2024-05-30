﻿using DailyActionCycle.WebUI.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace DailyActionCycle.WebUI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
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
        builder.Services.AddSingleton<TaskService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
