
using Carter;
using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using DailyActionCycle.WebAPI.Features.ActionTemplates;
using DailyActionCycle.WebAPI.Features.Days;
using DailyActionCycle.WebAPI.Features.ToDos;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Linq;

namespace DailyActionCycle.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DailyActionCycleDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5401;Database=dailyactioncycle;Username=postgres;Password=NotAll0wedForPublic");
            //options.UseSqlite("Data Source=dailyactioncycle.db");

        });

        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        builder.Services.AddCarter();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapCarter();


        //app.MapGet("/weatherforecast", (HttpContext httpContext) =>
        //{
        //    var forecast = Enumerable.Range(1, 5).ToArray();
        //    return forecast;
        //})
        //.WithName("GetWeatherForecast")
        //.WithOpenApi();

        //var actionTemplates = new[]
        //{
        //    new ActionTemplate()
        //    {
        //        Habits =
        //        [
        //            new Habit()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = "Drink water",
        //                Description = "Drink water",
        //                Daily = true,
        //                Weekly = false
        //            },
        //            new Habit()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = "Exercise",
        //                Weekly = false,
        //                Description = "Exercise",
        //                Daily = true
        //            }
        //        ],
        //        ToDos =
        //        [
        //            new ToDo()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = "Read",
        //                Description = "Read"
        //            },
        //            new ToDo()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = "Write",
        //                 Description = "Write"
        //            }
        //        ]
        //    }
        //};

        //app.MapGet("/actiontemplates", (HttpContext httpContext) =>
        //{
        //    var xs = actionTemplates.ToObservable();

        //    xs.Subscribe(Console.WriteLine);

        //    return actionTemplates;
        //});

        CreateToDo.MapEndpoint(app);

        CreateActionTemplate.MapEndpoint(app);
        GetActionTemplate.MapEndpoint(app);
        GetActionTemplates.MapEndpoint(app);
        AddHabitToActionTemplate.MapEndpoint(app);
        AddToDoToActionTemplate.MapEndpoint(app);

        AddDay.MapEndpoint(app);
        GetDay.MapEndpoint(app);
        SetActionTemplateToDay.MapEndpoint(app);

        app.Run();
    }
}