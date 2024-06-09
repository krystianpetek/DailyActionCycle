using DailyActionCycle.WebAPI.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebAPI.Features.Activities;

public static class GetActivities
{
    public class GetActivitiesQuery : IRequest<List<Activity>>;

    internal sealed class Handler : IRequestHandler<GetActivitiesQuery, List<Activity>>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Activity>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            var activities = await _dbContext.Activities.ToListAsync();

            return activities;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/activities", async (ISender sender) =>
        { 
            var activities = await sender.Send(new GetActivitiesQuery());

            return Results.Ok(activities);
        }).WithTags("Activities");
    }
}
