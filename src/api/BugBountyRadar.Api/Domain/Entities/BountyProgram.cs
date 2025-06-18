// Domain/Entities/BountyProgram.cs
namespace BugBountyRadar.Api.Domain.Entities;

public class BountyProgram
{
    public string Id { get; set; } = default!;   // agora string
    public string Platform { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal? MinReward { get; set; }
    public List<string> Technologies { get; set; } = new();
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}


