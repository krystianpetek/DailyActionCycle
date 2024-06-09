namespace DailyActionCycle.WebUI.Entities;

public sealed record class Activity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Title { get; set; }

    public required string Description { get; set; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime DueDate { get; set; } = DateTime.UtcNow.AddHours(1);

    public bool IsCompleted { get; set; } = false;

    public bool IsNotified { get; set; } = true;


    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }


    public void Complete() => IsCompleted = true;
}
