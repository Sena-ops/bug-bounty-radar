using BugBountyRadar.Api.Application.Import;
using BugBountyRadar.Api.Infrastructure.External.HackerOne;
using BugBountyRadar.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class HackerOneImporterTests
{
    [Fact]
    public async Task ImportAsync_StoresPrograms()
    {
        var mockClient = new Mock<IHackerOneClient>();
        var page = new HackerOnePageResponse(new List<HackerOneProgramWrapper>
        {
            new("1", new("Test", 10, ["dotnet"]))
        });
        mockClient.Setup(c => c.GetProgramsAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new Refit.ApiResponse<HackerOnePageResponse>(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK), page, null));

        var options = new DbContextOptionsBuilder<BbrContext>()
            .UseInMemoryDatabase("import")
            .Options;

        await using var db = new BbrContext(options);
        var mapper = new AutoMapper.Mapper(new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<HackerOneProfile>()));
        var logger = new LoggerFactory().CreateLogger<HackerOneImporter>();
        var importer = new HackerOneImporter(mockClient.Object, db, mapper, logger);

        var count = await importer.ImportAsync();
        Assert.Equal(1, count);
        Assert.Equal(1, await db.BountyPrograms.CountAsync());
    }
}
