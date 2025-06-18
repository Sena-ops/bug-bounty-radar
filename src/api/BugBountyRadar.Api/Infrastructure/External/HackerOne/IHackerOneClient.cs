using Refit;

namespace BugBountyRadar.Api.Infrastructure.External.HackerOne;

// DTOs
public record HackerOneProgramAttrs(
    string Name,
    [property: AliasAs("min_bounty")] decimal? MinBounty,
    string[]? Technology);

public record HackerOneProgramWrapper(
    string Id,
    HackerOneProgramAttrs Attributes);

public record HackerOnePageResponse(
    List<HackerOneProgramWrapper> Data);

// Interface com a rota “cola” os parâmetros
public interface IHackerOneClient
{
    [Get("/hackers/programs?page[number]={page}&page[size]={perPage}")]
    Task<ApiResponse<HackerOnePageResponse>> GetProgramsAsync(
        int page = 1,
        int perPage = 100);
}