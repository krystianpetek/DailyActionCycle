using DailyActionCycle.WebAPI.Database;
using DailyActionCycle.WebAPI.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebAPI.Features.ActionTemplates;

public static class GetActionTemplate
{
    public class GetActionTemplateQuery : IRequest<ActionTemplate?>
    {
        public Guid Id { get; init; }
    }

    internal sealed class Handler : IRequestHandler<GetActionTemplateQuery, ActionTemplate?>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionTemplate?> Handle(GetActionTemplateQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.ActionTemplates
                .Include(actionTemplate => actionTemplate.Activities)
                .FirstOrDefaultAsync(actionTemplate => actionTemplate.Id == request.Id, cancellationToken);
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/action-templates/{id:guid}", async ([FromRoute] Guid id, [FromServices] ISender sender) =>
        {
            var query = new GetActionTemplateQuery { Id = id };

            var actionTemplate = await sender.Send(query);

            return actionTemplate is not null
                ? Results.Ok(actionTemplate)
                : Results.NotFound();
        }).WithTags("ActionTemplates");
    }
}
