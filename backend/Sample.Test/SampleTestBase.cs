using dotenv.net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.App;
using Sample.Database;
using System.Data.Common;

namespace Sample.Test;

public class SampleTestBase : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        DotEnv.Load();
        Environment.SetEnvironmentVariable("FRONTEND_CORS_ADDRESS", "value");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SampleDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
            if (dbConnectionDescriptor != null)
            {
                services.Remove(dbConnectionDescriptor);
            }


            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection($"Data Source={Guid.NewGuid()}.db;");
                connection.Open();

                return connection;
            });

            services.AddDbContext<SampleDbContext>((serviceProvider, options) =>
            {
                var connection = serviceProvider.GetRequiredService<DbConnection>();
                options.UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });

            services.AddLogging(x =>
            {
                x.AddConsole();
                x.SetMinimumLevel(LogLevel.Information);
            });

            var serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database contexts
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;

            // Ensure the database is created.
            var dbContext = scopedServices.GetRequiredService<SampleDbContext>();
            dbContext.Database.Migrate();
        });

    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}