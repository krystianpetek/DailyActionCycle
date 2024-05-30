//using DailyActionCycle.Core.Entities;
//using DailyActionCycle.WebAPI.Database;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace DailyActionCycle.WebAPI.Features.Days;

//public static class MergeActionTemplateWithDay
//{
//    public class MergeActionTemplateWithDayCommand : IRequest<Day?>
//    {
//        public Guid DayId { get; set; }
//        public Guid ActionTemplateId { get; set; }
//    }

//    internal sealed class Handler : IRequestHandler<MergeActionTemplateWithDayCommand, Day?>
//    {
//        private readonly DailyActionCycleDbContext _dbContext;

//        public Handler(DailyActionCycleDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<Day?> Handle(MergeActionTemplateWithDayCommand request, CancellationToken cancellationToken)
//        {
//            var day = await _dbContext.Days
//                .Include(day => day.Tasks)
//                .Include(day => day.Habits)
//                .FirstOrDefaultAsync(day => day.Id == request.DayId, cancellationToken: cancellationToken);

//            if (day is null)
//                return default;

//            var actionTemplate = await _dbContext.ActionTemplates.FirstOrDefaultAsync(actionTemplate => actionTemplate.Id == request.ActionTemplateId, cancellationToken: cancellationToken);

//            if (actionTemplate is not null)
//            {
//                day.Tasks.AddRange(actionTemplate.Tasks.Select(toDo => toDo));
//                day.Habits.AddRange(actionTemplate.Habits.Select(habit => habit));
//            }

//            await _dbContext.SaveChangesAsync(cancellationToken);

//            return day;
//        }
//    }

//    public static void MapEndpoint(this IEndpointRouteBuilder app)
//    {
//        app.MapPost("api/days/{id:guid}/action-templates/{actionTemplateId:guid}", async ([FromRoute] Guid id, [FromRoute] Guid actionTemplateId, MergeActionTemplateWithDayCommand command, ISender sender) =>
//        {
//            command.DayId = id;
//            command.ActionTemplateId = actionTemplateId;

//            var day = await sender.Send(command);

//            return day is not null ? Results.Ok(day) : Results.NotFound();
//        });
//    }
//}
