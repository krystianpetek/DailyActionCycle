namespace DailyActionCycle.Core.Entities.Abstracts;
public record Entity
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool Completed { get; private set; } = false;

    public void Complete() => Completed = true;
}
