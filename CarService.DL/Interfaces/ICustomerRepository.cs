using CarService.Models.Entities;

namespace CarService.DL.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer); // Create

        Customer? GetCustomerById(int id);  // Read

        List<Customer> GetAllCustomers(); // Read All

        void UpdateCustomer(Customer customer); // Update

        void DeleteCustomer(int id); // Delete
    }
}