using CarService.DL.Interfaces;
using CarService.DL.MyStaticDB;
using CarService.Models.Entities;

namespace CarService.DL.Repositorities
{
    public class CustomerStaticRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                return;
            }

            StaticDB.Customers.Add(customer);
        }

        public Customer? GetCustomerById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return StaticDB.Customers.FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return StaticDB.Customers;
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetCustomerById(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);

            if (customer != null)
            {
                StaticDB.Customers.Remove(customer);
            }
        }
    }
}