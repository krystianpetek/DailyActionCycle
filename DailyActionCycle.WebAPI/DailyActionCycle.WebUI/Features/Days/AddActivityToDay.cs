using DailyActionCycle.WebUI.Database;
using DailyActionCycle.WebUI.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebUI.Features.Days;

public static class AddActivityToDay
{
    public class AddActivityToDayCommand : IRequest<Day>
    {
        internal DateOnly Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }
    }

    internal sealed class Handler : IRequestHandler<AddActivityToDayCommand, Day>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Day> Handle(AddActivityToDayCommand request, CancellationToken cancellationToken)
        {
            var day = await _dbContext.Days.FirstOrDefaultAsync(day => day.Date == request.Date, cancellationToken);

            var activity = new Activity
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate ?? DateTime.UtcNow.AddHours(1)
            };

            _dbContext.Activities.Add(activity);

            day.AddActivity(activity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return day;
        }
    }

    //public static void MapEndpoint(this IEndpointRouteBuilder app)
    //{
    //    app.MapPost("/days/{date:datetime}/activities", async (DateOnly Date, AddActivityToDayCommand command, ISender sender) =>
    //    {
    //        command.Date = Date;

    //        var day = await sender.Send(command);

    //        return Results.Ok(day);
    //    }).WithTags("Days");
    //}
}
