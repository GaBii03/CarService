using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Entities;

namespace CarService.BL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void AddCar(Car car)
        {
            _carRepository.AddCar(car);
        }

        public void DeleteCar(Guid id)
        {
            _carRepository.DeleteCar(id);
        }

        public List<Car> GetAllCars()
        {
            return _carRepository.GetAllCars();
        }

        public Car? GetCarById(Guid id)
        {
            return _carRepository.GetCarById(id);
        }

        public void UpdateCar(Car car)
        {
            _carRepository.UpdateCar(car);
        }
    }
}

