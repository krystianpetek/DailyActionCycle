using DailyActionCycle.WebUI.Entities;
using DailyActionCycle.WebUI.Database;
using MediatR;

namespace DailyActionCycle.WebUI.Features.Activities;

public static class CreateActivity
{
    public class CreateActivityCommand : IRequest<Guid>
    {
        internal Guid Id { get; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Description { get; set; }
    }

    internal sealed class Handler : IRequestHandler<CreateActivityCommand, Guid>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = new Activity
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description
            };

            _dbContext.Activities.Add(activity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return activity.Id;
        }
    }

    //public static void MapEndpoint(this IEndpointRouteBuilder app)
    //{
    //    app.MapPost("/activities", async (CreateActivityCommand command, ISender sender) =>
    //    {
    //        var toDoId = await sender.Send(command);

    //        return Results.Ok(toDoId);
    //    }).WithTags("Activities");
    //}
}
