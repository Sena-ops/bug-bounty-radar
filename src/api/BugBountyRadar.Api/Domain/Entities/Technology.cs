// Domain/Entities/Technology.cs
namespace BugBountyRadar.Api.Domain.Entities;

public class Technology
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<BountyProgram> Programs { get; set; } = [];
}