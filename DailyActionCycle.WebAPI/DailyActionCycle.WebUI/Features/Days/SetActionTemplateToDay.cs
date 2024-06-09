using DailyActionCycle.WebUI.Database;
using DailyActionCycle.WebUI.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebUI.Features.Days;

public static class SetActionTemplateToDay
{
    public class SetActionTemplateToDayCommand : IRequest<Day>
    {
        internal DateOnly Date { get; set; }

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
                .Include(day => day.Activities)
                .Include(day => day.ActionTemplate)
                .FirstOrDefaultAsync(day => day.Date == request.Date, cancellationToken);

            var actionTemplate = await _dbContext.ActionTemplates
                .Include(at => at.Activities)
                .FirstOrDefaultAsync(at => at.Id == request.ActionTemplateId, cancellationToken);

            if (day.ActionTemplate is not null)
            {
                foreach (var activity in day.ActionTemplate.Activities)
                {
                    day.RemoveActivity(activity);
                }
            }

            if (actionTemplate is not null)
            {
                day.ActionTemplate = actionTemplate;
                day.Activities.AddRange(actionTemplate.Activities.Select(activity => activity));
            }

            var updatedEntity = _dbContext.Days.Update(day);

            await _dbContext.SaveChangesAsync(cancellationToken);

            updatedEntity.Entity.ActionTemplate = null;

            return day;
        }
    }

    //public static void MapEndpoint(this IEndpointRouteBuilder app)
    //{
    //    app.MapPut("/days/{date:datetime}/action-templates/{actionTemplateId:guid}/", async (DateOnly Date, Guid ActionTemplateId, ISender sender) =>
    //    {
    //        var command = new SetActionTemplateToDayCommand()
    //        {
    //            Date = Date,
    //            ActionTemplateId = ActionTemplateId,
    //        };

    //        var day = await sender.Send(command);

    //        return Results.Ok(day);
    //    }).WithTags("Days");
    //}
}
