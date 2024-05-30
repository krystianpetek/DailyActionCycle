using Carter;
using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;

namespace DailyActionCycle.WebAPI.Features.ActionTemplates;

public static class CreateActionTemplate
{
    public class CreateActionTemplateCommand : IRequest<Guid>
    {
        internal Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
    }

    internal sealed class Handler : IRequestHandler<CreateActionTemplateCommand, Guid>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateActionTemplateCommand request, CancellationToken cancellationToken)
        {
            var actionTemplate = new ActionTemplate
            {
                Id = request.Id,
                Name = request.Name,
                Habits = [],
                Tasks = []
            };

            _dbContext.ActionTemplates.Add(actionTemplate);
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return actionTemplate.Id;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/action-templates", async (CreateActionTemplateCommand command, ISender sender) =>
        {
            var actionTemplateId = await sender.Send(command);

            return Results.Ok(actionTemplateId);
        });
    }

    //public class Endpoint : ICarterModule
    //{
    //    public void AddRoutes(IEndpointRouteBuilder app)
    //    {
    //        app.MapPost("api/action-templates", async (Command command, ISender sender) =>
    //        {
    //            var actionTemplateId = await sender.Send(command);
                
    //            return Results.Ok(actionTemplateId);
    //        });
    //    }
    //}
}
