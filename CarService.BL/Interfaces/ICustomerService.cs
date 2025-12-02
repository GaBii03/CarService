using CarService.Models.Entities;

namespace CarService.BL.Interfaces
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer); // Create

        Customer? GetCustomerById(int id);  // Read

        List<Customer> GetAllCustomers(); // Read All

        void UpdateCustomer(Customer customer); // Update

        void DeleteCustomer(int id); // Delete
    }


}