using System.Net;
using BugBountyRadar.Api.Domain.Entities;
using BugBountyRadar.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ProgramsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProgramsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<BbrContext>(options =>
                    options.UseInMemoryDatabase("test"));
            });
        });
    }

    [Fact]
    public async Task Get_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var resp = await client.GetAsync("/programs");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }
}
