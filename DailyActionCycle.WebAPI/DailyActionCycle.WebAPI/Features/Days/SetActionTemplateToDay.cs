using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebAPI.Features.Days;

public static class SetActionTemplateToDay
{
    public class SetActionTemplateToDayCommand : IRequest<Day>
    {
        internal Guid Id { get; set; }

        internal Guid ActionTemplateId { get; init; }
    }

    internal sealed class Handler : IRequestHandler<SetActionTemplateToDayCommand, Day>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Day> Handle(SetActionTemplateToDayCommand request, CancellationToken cancellationToken)
        {
            var day = await _dbContext.Days
                .Include(day => day.Tasks)
                .Include(day => day.Habits)
                .FirstOrDefaultAsync(day => day.Id == request.Id, cancellationToken);

            var actionTemplate = await _dbContext.ActionTemplates
                .Include(at => at.Tasks)
                .Include(at => at.Habits)
                .FirstOrDefaultAsync(at => at.Id == request.ActionTemplateId, cancellationToken);

            if (actionTemplate is not null)
            {
                day.ActionTemplate = actionTemplate;
                day.Tasks.Clear();
                day.Tasks.AddRange(actionTemplate.Tasks.Select(toDo => toDo));

                day.Habits.Clear();
                day.Habits.AddRange(actionTemplate.Habits.Select(habit => habit));
            }

            _dbContext.Days.Update(day);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return day;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("api/days/{id:guid}/action-templates/{actionTemplateId:guid}/", async (Guid Id, Guid ActionTemplateId, ISender sender) =>
        {
            var command = new SetActionTemplateToDayCommand()
            {
                Id = Id,
                ActionTemplateId = ActionTemplateId,
            };

            var day = await sender.Send(command);

            return Results.Ok(day);
        });
    }
}
