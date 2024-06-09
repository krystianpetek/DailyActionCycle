using DailyActionCycle.WebUI.Database;
using DailyActionCycle.WebUI.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebUI.Features.Activities;

public static class ModifyActivity
{
    public class ModifyActivityCommand : IRequest<Activity>
    {
        internal Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool? IsCompleted { get; set; }

        public bool? IsNotified { get; set; }

        internal DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool? IsDeleted { get; set; }
    }

    internal sealed class Handler : IRequestHandler<ModifyActivityCommand, Activity>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Activity> Handle(ModifyActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dbContext.Activities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (request.Title is not null)
                activity.Title = request.Title;

            if (request.Description is not null)
                activity.Description = request.Description;

            if (request.DueDate is not null)
                activity.DueDate = request.DueDate.Value;

            if (request.IsCompleted is not null)
                activity.IsCompleted = request.IsCompleted.Value;

            if (request.IsNotified is not null)
                activity.IsNotified = request.IsNotified.Value;

            if (request.IsDeleted is not null)
            {
                activity.IsDeleted = request.IsDeleted.Value;
                activity.DeletedAt = DateTime.UtcNow;
            }

            activity.UpdatedAt = request.UpdatedAt;

            _dbContext.Activities.Update(activity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return activity;
        }
    }

    //public static void MapEndpoint(this IEndpointRouteBuilder app)
    //{
    //    app.MapPut("/activities/{id:guid}", async (Guid Id, ModifyActivityCommand command, ISender sender) =>
    //    {
    //        command.Id = Id;

    //        var activity = await sender.Send(command);

    //        return Results.Ok(activity);
    //    }).WithTags("Activities");
    //}
}
