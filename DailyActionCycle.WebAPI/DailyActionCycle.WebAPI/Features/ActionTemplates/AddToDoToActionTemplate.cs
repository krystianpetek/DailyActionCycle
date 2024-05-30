using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebAPI.Features.ActionTemplates;

public static class AddToDoToActionTemplate
{
    public class AddToDoToActionTemplateCommand : IRequest<ActionTemplate>
    {
        internal Guid ActionTemplateId { get; set; }
        
        public Guid ToDoId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<AddToDoToActionTemplateCommand, ActionTemplate>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionTemplate> Handle(AddToDoToActionTemplateCommand request, CancellationToken cancellationToken)
        {
            var actionTemplate = await _dbContext.ActionTemplates
                .Include(actionTemplate => actionTemplate.Tasks)
                .FirstOrDefaultAsync(actionTemplate => actionTemplate.Id == request.ActionTemplateId, cancellationToken);

            if (actionTemplate is null)
                throw new InvalidOperationException("Action template not found.");

            var toDo = await _dbContext.ToDos.FirstOrDefaultAsync(todo => todo.Id == request.ToDoId, cancellationToken);

            if (toDo is not null && !actionTemplate.Tasks.Any(todo => todo.Id == request.ToDoId))
                actionTemplate.Tasks.Add(toDo);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return actionTemplate;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/action-templates/{id:guid}/to-dos", async ([FromRoute] Guid id, AddToDoToActionTemplateCommand command, ISender sender) =>
        {
            command.ActionTemplateId = id;

            var toDoId = await sender.Send(command);

            return Results.Ok(toDoId);
        });
    }

}
