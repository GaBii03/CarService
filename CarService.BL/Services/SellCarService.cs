using CarService.BL.Interfaces;
using CarService.DL.Kafka;
using CarService.Models.Messages;
using CarService.Models.Responses;

namespace CarService.BL.Services
{
    internal class SellCarService : ISellCarService
    {
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;
        private readonly IKafkaProducer<SellCarMessage> _kafkaProducer;

        public SellCarService(
            ICustomerService customerService,
            ICarService carService,
            IKafkaProducer<SellCarMessage> kafkaProducer)
        {
            _customerService = customerService;
            _carService = carService;
            _kafkaProducer = kafkaProducer;
        }

        public SellCarResponse SellCar(Guid carId, Guid customerId)
        {
            var car = _carService.GetCarById(carId);
            var customer = _customerService.GetById(customerId);

            if (car == null || customer == null)
            {
                throw new ArgumentException("Invalid car or customer ID.");
            }

            var salePrice = car.BasePrice - customer.Discount;
            if (salePrice < 0) salePrice = 0;

            _ = _kafkaProducer.ProduceAsync(
                carId.ToString(),
                new SellCarMessage(car.Model, customer.Name, salePrice));

            return new SellCarResponse
            {
                Customer = customer,
                Car = car,
                SalePrice = salePrice
            };
        }
    }
}