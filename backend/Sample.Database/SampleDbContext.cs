using dotenv.net.Utilities;
using Microsoft.EntityFrameworkCore;
using Sample.Database.Models;

namespace Sample.Database;

public class SampleDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(EnvReader.GetStringValue("DB_CONNECTION_STRING"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<EventDbm> Events { get; set; }
}
