using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;

namespace DailyActionCycle.WebAPI.Features.Days;

public static class AddDay
{
    public class AddDayCommand : IRequest<Day>
    {
        internal Guid Id { get; } = Guid.NewGuid();

        public DateOnly Date { get; set; }
    }

    internal sealed class Handler : IRequestHandler<AddDayCommand, Day>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Day> Handle(AddDayCommand request, CancellationToken cancellationToken)
        {
            var day = new Day(request.Date);

            _dbContext.Days.Add(day);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return day;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        //app.MapPost("api/days", async (AddDayCommand command, ISender sender) =>
        //{
        //    var day = await sender.Send(command);

        //    return Results.Ok(day);
        //});
    }
}
