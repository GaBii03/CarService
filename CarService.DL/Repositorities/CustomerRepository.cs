using System;
using System.Collections.Generic;
using System.Linq;
using CarService.DL.Interfaces;
using CarService.Models.Configurations;
using CarService.Models.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarService.DL.Repositorities
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IMongoCollection<Customer> _customersCollection;
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            IMongoClient mongoClient,
            ILogger<CustomerRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;
            var database = mongoClient.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);
            _customersCollection = database.GetCollection<Customer>($"{nameof(Customer)}s");
        }

        public void Add(Customer? customer)
        {
            if (customer == null) return;

            try
            {
                _customersCollection.InsertOneAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding customer");
                throw;
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                return _customersCollection.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all customers");
                throw;
            }
        }

        public Customer? GetById(Guid id)
        {
            if (id == Guid.Empty) return null;

            return _customersCollection.Find(c => c.Id == id).FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty) return;

            _customersCollection.DeleteOne(c => c.Id == id);
        }
    }
}