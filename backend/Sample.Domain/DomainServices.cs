using dotenv.net.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Database;
using Sample.Domain.Events;

namespace Sample.Domain;

public class DomainServices
{
    public static void Register(IServiceCollection services)
    {
        services.AddDbContext<SampleDbContext>(options =>
        {
            options.UseSqlite(EnvReader.GetStringValue("DB_CONNECTION_STRING"));
        });

        services.AddScoped<CreateEvent>();
    }

    public static void InitDatabase(IServiceProvider serviceProvider)
    {
        try
        {
            using var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<SampleDbContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Migrations detected, applying...");
                dbContext.Database.Migrate();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while migrating the database.");
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
