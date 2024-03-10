using Microsoft.EntityFrameworkCore;
using Sample.Database.Models;

namespace Sample.Database;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions options)
        :base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<EventDbm> Events { get; set; }
}
