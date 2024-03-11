using dotenv.net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.App;
using Sample.Database;

namespace Sample.Test;

public class SampleTestBase : WebApplicationFactory<Program>
{
    private readonly string _dbDame = Guid.NewGuid().ToString();
    private bool _disposed = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        DotEnv.Load();
        Environment.SetEnvironmentVariable("FRONTEND_CORS_ADDRESS", "value");

        builder.ConfigureServices(services =>
        {
            ReplaceDatabase(services);
            MigrateDatabase(services);

        });

    }

    private static void MigrateDatabase(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<SampleDbContext>();
        dbContext.Database.Migrate();
    }

    private void ReplaceDatabase(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SampleDbContext>));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<SampleDbContext>((serviceProvider, options) =>
        {

            options.UseSqlite($"Data Source={_dbDame}.db;");
        });
    }

    protected override void Dispose(bool disposing)
    {    
        if (!_disposed)
        {
            using var scope = this.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<SampleDbContext>();
            dbContext.Database.EnsureDeleted();
            _disposed = true;
        }
        base.Dispose(disposing);
    }
}