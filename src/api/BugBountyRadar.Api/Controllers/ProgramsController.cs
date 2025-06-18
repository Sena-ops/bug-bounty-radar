using BugBountyRadar.Api.Domain.Entities;
using BugBountyRadar.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("programs")]
public class ProgramsController : ControllerBase
{
    private readonly BbrContext _db;
    public ProgramsController(BbrContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<BountyProgram>> Get() =>
        await _db.BountyPrograms
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();
}
