using System.ComponentModel.DataAnnotations;

namespace BugBountyRadar.Api.Controllers;

public class ProgramUpdateDto
{
    [Required]
    [StringLength(200)]
    public string? Name { get; set; }
}
