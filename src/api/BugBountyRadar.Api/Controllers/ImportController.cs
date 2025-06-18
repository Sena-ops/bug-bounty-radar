using BugBountyRadar.Api.Application.Import;
using Microsoft.AspNetCore.Mvc;

namespace BugBountyRadar.Api.Controllers;

[ApiController]
[Route("import")]
public class ImportController(HackerOneImporter importer) : ControllerBase
{
    [HttpPost("hackerone")]
    public async Task<IActionResult> ImportH1()
    {
        var count = await importer.ImportAsync();
        return Ok(new { imported = count });
    }
}