using CarService.Models.Entities;

namespace CarService.DL.Interfaces
{
    public interface ICarRepository
    {
        void AddCar(Car car); // Create

        Car? GetCarById(Guid id);  // Read

        List<Car> GetAllCars(); // Read All

        void UpdateCar(Car car); // Update

        void DeleteCar(Guid id); // Delete
    }
}

