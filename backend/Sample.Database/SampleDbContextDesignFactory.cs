using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sample.Database;

public class SampleDbContextDesignFactory : IDesignTimeDbContextFactory<SampleDbContext>
{
    public SampleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SampleDbContext>();
        optionsBuilder.UseSqlite("Data Source=design.db");

        return new SampleDbContext(optionsBuilder.Options);
    }
}
