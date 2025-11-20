using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Entities;

namespace CarService.BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.DeleteCustomer(id);
        }

        public List<Customer> GetAllCustomers()
        {
            _customerRepository.GetAllCustomers();
            return null;
        }

        public Customer? GetCustomerById(int id)
        {
            _customerRepository.GetCustomerById(id);
            return null;
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }
    }
}