using BugBountyRadar.Api.Domain.Entities;
using BugBountyRadar.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("programs")]
public class ProgramsController(BbrContext db) : ControllerBase
{
    [HttpGet]
    public async Task<List<BountyProgram>> Get() =>
        await db.Programs
            .Include(p => p.Technologies)
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();
}