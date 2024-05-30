using DailyActionCycle.Core.Entities;
using DailyActionCycle.WebAPI.Database;
using MediatR;

namespace DailyActionCycle.WebAPI.Features.ToDos;

public static class CreateToDo
{
    public class CreateToDoCommand : IRequest<Guid>
    {
        internal Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
        
        public string Description { get; set; }
    }

    internal sealed class Handler : IRequestHandler<CreateToDoCommand, Guid>
    {
        private readonly DailyActionCycleDbContext _dbContext;

        public Handler(DailyActionCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
        {
            var toDo = new ToDo
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };

            _dbContext.ToDos.Add(toDo);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return toDo.Id;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/to-dos", async (CreateToDoCommand command, ISender sender) =>
        {
            var toDoId = await sender.Send(command);

            return Results.Ok(toDoId);
        });
    }
}
