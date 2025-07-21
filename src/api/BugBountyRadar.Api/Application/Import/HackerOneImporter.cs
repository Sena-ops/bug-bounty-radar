using AutoMapper;
using BugBountyRadar.Api.Domain.Entities;
using BugBountyRadar.Api.Infrastructure.External.HackerOne;
using BugBountyRadar.Api.Infrastructure.Persistence;

namespace BugBountyRadar.Api.Application.Import;

public class HackerOneImporter(
    IHackerOneClient client,
    BbrContext db,
    IMapper mapper,
    ILogger<HackerOneImporter> log)
{
    public async Task<int> ImportAsync()
    {
        var page     = 1;
        var imported = 0;

        while (true)
        {
            var resp = await client.GetProgramsAsync(page);

            log.LogInformation("H1 page {p} -> {code}", page, resp.StatusCode);

            var wrappers = resp.Content?.Data;
            if (wrappers is null || wrappers.Count == 0) break;

            var entities = mapper.Map<List<BountyProgram>>(wrappers);

            foreach (var e in entities)
            {
                var existing = await db.BountyPrograms.FindAsync(e.Id);
                if (existing is null)
                    db.BountyPrograms.Add(e);
                else
                    db.Entry(existing).CurrentValues.SetValues(e);
            }

            imported += await db.SaveChangesAsync();
            page++;
        }

        log.LogInformation("H1 import finished. {count} programs upserted", imported);
        return imported;
    }
}
