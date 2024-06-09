using DailyActionCycle.WebUI.Database;
using DailyActionCycle.WebUI.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyActionCycle.WebUI.Features.ActionTemplates;

public static class GetActionTemplates
{
    public class GetActionTemplatesQuery : IRequest<List<ActionTemplate>>
    {
        public Guid Id { get; init; }
    }

    internal sealed class Handler : IRequestHandler<GetActionTemplatesQuery, List<ActionTemplate>>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ActionTemplate>> Handle(GetActionTemplatesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.ActionTemplates
                .Include(actionTemplate => actionTemplate.Activities)
                .ToListAsync(cancellationToken);
        }
    }

    //public static void MapEndpoint(this IEndpointRouteBuilder app)
    //{
    //    app.MapGet("/action-templates", async ([FromServices] ISender sender) =>
    //    {
    //        var query = new GetActionTemplatesQuery();

    //        var actionTemplate = await sender.Send(query);

    //        return actionTemplate is not null
    //            ? Results.Ok(actionTemplate)
    //            : Results.NotFound();
    //    }).WithTags("ActionTemplates");
    //}
}
