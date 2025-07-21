using BugBountyRadar.Api.Domain.Entities;
using BugBountyRadar.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugBountyRadar.Api.Controllers;

[ApiController]
[Route("programs")]
public class ProgramsController(BbrContext db) : ControllerBase
{
    /// <summary>
    ///     Retrieves programs with optional search and pagination.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BountyProgram>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string sort = "name")
    {
        if (page < 1 || pageSize < 1)
            return BadRequest(new { message = "Invalid paging parameters" });

        var query = db.BountyPrograms.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search));

        query = sort.ToLowerInvariant() switch
        {
            "updated" => query.OrderByDescending(p => p.UpdatedAtUtc),
            _ => query.OrderBy(p => p.Name)
        };

        var results = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(results);
    }

    /// <summary>Gets a program by id.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BountyProgram), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var program = await db.BountyPrograms.FindAsync(id);
        return program is null ? NotFound() : Ok(program);
    }

    /// <summary>Updates a program name.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BountyProgram), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string id, ProgramUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var program = await db.BountyPrograms.FindAsync(id);
        if (program is null)
            return NotFound();

        program.Name = dto.Name!;
        await db.SaveChangesAsync();
        return Ok(program);
    }

    /// <summary>Deletes a program.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var program = await db.BountyPrograms.FindAsync(id);
        if (program is null)
            return NotFound();

        db.BountyPrograms.Remove(program);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
