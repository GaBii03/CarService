using CarService.DL.Interfaces;
using CarService.DL.MyStaticDB;
using CarService.Models.Entities;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Repositorities
{
    internal class CustomerStaticRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerStaticRepository> _logger;

        public CustomerStaticRepository(ILogger<CustomerStaticRepository> logger)
        {
            _logger = logger;
        }

        public void Add(Customer? customer)
        {
            if (customer == null) return;

            StaticDB.Customers.Add(customer);
        }

        public List<Customer> GetAll()
        {
            try
            {
                return StaticDB.Customers;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}-{e.StackTrace}");
            }

            return new List<Customer>();
        }

        public Customer? GetById(Guid id)
        {
            if (id == Guid.Empty) return null;

            return StaticDB.Customers
                .FirstOrDefault(c => c.Id == id);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty) return;

            var customer = GetById(id);

            if (customer != null)
            {
                StaticDB.Customers.Remove(customer);
            }
        }
    }
}