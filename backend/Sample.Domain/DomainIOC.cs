using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Database;

namespace Sample.Domain;

public class DomainIOC
{
    public static void Register(IServiceCollection services)
    {
        services.AddDbContext<SampleDbContext>();
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
