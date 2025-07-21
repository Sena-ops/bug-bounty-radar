using BugBountyRadar.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugBountyRadar.Api.Infrastructure.Persistence;

public class BbrContext(DbContextOptions<BbrContext> options) : DbContext(options)
{
    public DbSet<BountyProgram> Programs  => Set<BountyProgram>();
    public DbSet<Technology> Technologies => Set<Technology>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BountyProgram>(b =>
        {
            b.Property(p => p.Technologies)
                .HasColumnType("text[]");
        });
    }

    public DbSet<BountyProgram> BountyPrograms => Set<BountyProgram>();
}
