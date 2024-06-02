
using DailyActionCycle.WebAPI.Database;
using DailyActionCycle.WebAPI.Features.ActionTemplates;
using DailyActionCycle.WebAPI.Features.Activities;
using DailyActionCycle.WebAPI.Features.Days;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Linq;

namespace DailyActionCycle.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DailyActionCycleDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5401;Database=dailyactioncycle;Username=postgres;Password=NotAll0wedForPublic");
        });

        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        //app.MapGet("/actiontemplates", (HttpContext httpContext) =>
        //{
        //    var xs = actionTemplates.ToObservable();

        //    xs.Subscribe(Console.WriteLine);

        //    return actionTemplates;
        //});

        var apiGroup = app.MapGroup("/api");

        CreateActivity.MapEndpoint(apiGroup);
        ModifyActivity.MapEndpoint(apiGroup);

        CreateActionTemplate.MapEndpoint(apiGroup);
        GetActionTemplate.MapEndpoint(apiGroup);
        GetActionTemplates.MapEndpoint(apiGroup);
        AddActivityToActionTemplate.MapEndpoint(apiGroup);

        GetDay.MapEndpoint(apiGroup);
        SetActionTemplateToDay.MapEndpoint(apiGroup);
        AddActivityToDay.MapEndpoint(apiGroup);

        app.Urls.Add("https://0.0.0.0:7207/");

        app.Run();
    }
}