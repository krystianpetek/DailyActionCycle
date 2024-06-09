using DailyActionCycle.WebUI.Database;
using DailyActionCycle.WebUI.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebUI.Features.ActionTemplates;

public static class AddActivityToActionTemplate
{
    public class AddActivityToActionTemplateCommand : IRequest<ActionTemplate>
    {
        internal Guid ActionTemplateId { get; set; }

        public Guid ActivityId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<AddActivityToActionTemplateCommand, ActionTemplate>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionTemplate> Handle(AddActivityToActionTemplateCommand request, CancellationToken cancellationToken)
        {
            var actionTemplate = await _dbContext.ActionTemplates
                .Include(actionTemplate => actionTemplate.Activities)
                .FirstOrDefaultAsync(actionTemplate => actionTemplate.Id == request.ActionTemplateId, cancellationToken);

            if (actionTemplate is null)
                throw new InvalidOperationException("Action template not found.");

            var activity = await _dbContext.Activities.FirstOrDefaultAsync(todo => todo.Id == request.ActivityId, cancellationToken);

            if (activity is not null && !actionTemplate.Activities.Any(activity => activity.Id == request.ActivityId))
                actionTemplate.Activities.Add(activity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return actionTemplate;
        }
    }

    //public static void MapEndpoint(this IEndpointRouteBuilder app)
    //{
    //    app.MapPost("/action-templates/{id:guid}/activities", async ([FromRoute] Guid id, AddActivityToActionTemplateCommand command, ISender sender) =>
    //    {
    //        command.ActionTemplateId = id;

    //        var toDoId = await sender.Send(command);

    //        return Results.Ok(toDoId);
    //    }).WithTags("ActionTemplates");
    //}

}
