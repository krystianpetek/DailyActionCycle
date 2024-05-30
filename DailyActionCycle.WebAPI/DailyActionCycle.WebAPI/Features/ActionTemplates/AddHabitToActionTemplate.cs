using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebAPI.Features.ActionTemplates;

public static class AddHabitToActionTemplate
{
    public class AddHabitToActionTemplateCommand : IRequest<ActionTemplate>
    {
        public Guid ActionTemplateId { get; set; }
        
        public Guid HabitId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<AddHabitToActionTemplateCommand, ActionTemplate>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionTemplate> Handle(AddHabitToActionTemplateCommand request, CancellationToken cancellationToken)
        {
            var actionTemplate = await _dbContext.ActionTemplates
                .Include(actionTemplate => actionTemplate.Habits)
                .FirstOrDefaultAsync(actionTemplate => actionTemplate.Id == request.ActionTemplateId, cancellationToken);

            if (actionTemplate is null)
                throw new InvalidOperationException("Action template not found.");

            var habit = await _dbContext.Habits.FirstOrDefaultAsync(todo => todo.Id == request.HabitId, cancellationToken);

            if(habit is not null)
                actionTemplate.Habits.Add(habit);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return actionTemplate;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/action-templates/{id:guid}/habits", async ([FromRoute] Guid id, AddHabitToActionTemplateCommand command, ISender sender) =>
        {
            command.ActionTemplateId = id;

            var toDoId = await sender.Send(command);

            return Results.Ok(toDoId);
        });
    }

}
