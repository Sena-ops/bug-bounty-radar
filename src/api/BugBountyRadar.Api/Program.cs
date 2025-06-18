using System.Text;
using System.Net.Http;
using BugBountyRadar.Api.Infrastructure.Persistence;
using BugBountyRadar.Api.Infrastructure.External.HackerOne;
using BugBountyRadar.Api.Application.Import;
using BugBountyRadar.Api.Background;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using Refit;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Swagger + Controllers + EF
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BbrContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// AutoMapper (procura qualquer Profile no assembly)
builder.Services.AddAutoMapper(typeof(Program));

// HackerOne HTTP client + Polly
builder.Services.AddRefitClient<IHackerOneClient>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://api.hackerone.com/v1");
        var user  = builder.Configuration["H1:Username"];
        var token = builder.Configuration["H1:ApiToken"];
        var basic = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{user}:{token}"));
        c.DefaultRequestHeaders.Authorization =
            new("Basic", basic);
    })
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry))));
// DI
builder.Services.AddScoped<HackerOneImporter>();
builder.Services.AddHostedService<HackerOneSyncJob>();   // opcional

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));

app.Run();