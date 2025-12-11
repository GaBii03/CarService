using CarService.Models.Entities;

namespace CarService.DL.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer? customer);
        List<Customer> GetAll();
        Customer? GetById(Guid id);
        void Delete(Guid id);
    }
}