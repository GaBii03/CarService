using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CarService.DL.Interfaces;
using CarService.DL.Repositorities;
using CarService.Models.Configurations;
using MongoDB.Driver;

namespace CarService.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, CustomerStaticRepository>();
            services.AddSingleton<ICarRepository, CarMongoRepository>();
            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration"));
            
            // Register MongoClient with connection string from configuration
            var mongoConfig = configuration.GetSection("MongoDbConfiguration").Get<MongoDbConfiguration>();
            if (mongoConfig != null)
            {
                services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConfig.ConnectionString));
            }
            else
            {
                services.AddSingleton<IMongoClient>(_ => new MongoClient());
            }
            
            return services;
        }

    }
}

