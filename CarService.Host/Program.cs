using CarService.BL;
using CarService.DL;
using CarService.DL.Interfaces;
using Mapster;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace CarService.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services
                .AddConfigurations(builder.Configuration)
                .AddDataLayer()
                .AddBusinessLogicLayer();

            builder.Services.AddMapster();

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Service", Version = "v1" });
            });


            var app = builder.Build();
            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseSwagger();
            
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "Car Service V1");
            });

            app.Run();
        }
    }
}
