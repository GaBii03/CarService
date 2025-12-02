using CarService.Models.Entities;

namespace CarService.BL.Interfaces
{
    public interface ICarService
    {
        void AddCar(Car car); // Create

        Car? GetCarById(Guid id);  // Read

        List<Car> GetAllCars(); // Read All

        void UpdateCar(Car car); // Update

        void DeleteCar(Guid id); // Delete
    }
}

