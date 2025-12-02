using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.DL.Interfaces;
using CarService.DL.MyStaticDB;
using CarService.Models.Configurations;
using CarService.Models.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarService.DL.Repositorities
{
    public class CarMongoRepository : ICarRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<CarMongoRepository> _logger;

        private readonly IMongoCollection<Car> _carsCollection;

        public CarMongoRepository(IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
                                  ILogger<CarMongoRepository> logger,
                                  IMongoClient mongoClient)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;
            var database = mongoClient.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);
            _carsCollection = database.GetCollection<Car>($"{nameof(Car)}s");
        }


        public void AddCar(Car car)
        {
            if (car == null) return;
            try
            {
                _carsCollection.InsertOne(car);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding car");
                throw;
            }

        }

        public void DeleteCar(Guid id)
        {
            if (id == Guid.Empty) return;
            try
            {
                var result = _carsCollection.DeleteOne(c => c.Id == id);
                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No car found with id {id} to delete");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car");
                throw;
            }
        }

        public List<Car> GetAllCars()
        {
            return _carsCollection.Find(_ => true).ToList();
        }

        public Car? GetCarById(Guid id)
        {
            if (id == Guid.Empty) return null;
            try
            {
                return _carsCollection.Find(c => c.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving car by id");
                throw;
            }
        }

        public void UpdateCar(Car car)
        {
            _carsCollection.ReplaceOne(c => c.Id == car.Id, car);
        }
    }
}