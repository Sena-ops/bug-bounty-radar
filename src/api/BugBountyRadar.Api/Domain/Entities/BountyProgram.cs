// Domain/Entities/BountyProgram.cs
namespace BugBountyRadar.Api.Domain.Entities;

public class BountyProgram
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Platform { get; set; }   // "HackerOne", "Bugcrowd"…
    public required string Name { get; set; }
    public decimal? MinReward { get; set; }
    public List<Technology> Technologies { get; set; } = [];
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}

