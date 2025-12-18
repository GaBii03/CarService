using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CarService.DL.Interfaces;
using CarService.DL.Repositorities;
using CarService.Models.Configurations;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

namespace CarService.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
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

