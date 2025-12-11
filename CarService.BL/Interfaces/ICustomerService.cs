using CarService.Models.Entities;

namespace CarService.BL.Interfaces
{
    public interface ICustomerService
    {
        void Add(Customer? customer);
        List<Customer> GetAll();
        Customer? GetById(Guid id);
        void Delete(Guid id);
    }
}