using BugBountyRadar.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BbrContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Se tiver controllers (ex.: ProgramsController)
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();          // <- ponto único de roteamento
app.MapGet("/healthz", () => Results.Ok(new { status = "ok", ts = DateTime.UtcNow }));

app.Run();