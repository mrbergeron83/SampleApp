
using dotenv.net;
using Sample.Domain;

namespace Sample.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sampleCorsPolicy = "SampleCorsPolicy";
            DotEnv.Load();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: sampleCorsPolicy,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            // Register app services through inversion of control
            DomainIOC.Register(builder.Services);

            var app = builder.Build();

            DomainIOC.InitDatabase(app.Services);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(sampleCorsPolicy);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
