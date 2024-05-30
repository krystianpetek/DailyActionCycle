using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebAPI.Features.Days;

public static class GetDay
{
    public record Query(DateOnly Date) : IRequest<Day?>;

    internal sealed class Handler : IRequestHandler<Query, Day?>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Day?> Handle(Query request, CancellationToken cancellationToken)
        {
            var day = await _dbContext.Days
                .Include(day => day.Tasks)
                .Include(day => day.Habits)
                .FirstOrDefaultAsync(day => day.Date == request.Date, cancellationToken);

            if(day is null)
            {
                day = new Day(request.Date);
                _dbContext.Days.Add(day);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return day;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/days/{date:datetime}", async (DateTime date, ISender sender) =>
        {
            var query = new Query(DateOnly.FromDateTime(date));

            var day = await sender.Send(query);

            return day is not null ? Results.Ok(day) : Results.NotFound();
        });
    }   
}
