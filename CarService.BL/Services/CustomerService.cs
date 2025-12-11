using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Entities;

namespace CarService.BL.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Add(Customer? customer)
        {
            if (customer == null) return;

            customer.Id = Guid.NewGuid();

            _customerRepository.Add(customer);
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetById(Guid id)
        {
            return _customerRepository.GetById(id);
        }

        public void Delete(Guid id)
        {
            _customerRepository.Delete(id);
        }
    }
}