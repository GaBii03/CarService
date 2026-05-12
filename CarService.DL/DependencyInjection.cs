using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CarService.DL.Interfaces;
using CarService.DL.Repositorities;
using CarService.DL.Infrastructure;
using CarService.DL.Kafka;
using CarService.Models.Configurations;
using CarService.Models.Messages;

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

            services.AddHostedService<CarHostedService>();
            services.AddHostedService<CarBackgroundService>();

            services.AddSingleton<IKafkaProducer<ChatMessage>, KafkaProducer<ChatMessage>>();
            services.AddHostedService<KafkaConsumer<ChatMessage>>();

            services.AddSingleton<IKafkaProducer<SellCarMessage>, KafkaProducer<SellCarMessage>>();
            services.AddHostedService<SellCarKafkaConsumer>();

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration"));
            services.Configure<KafkaConfiguration>(configuration.GetSection("KafkaConfiguration"));

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

