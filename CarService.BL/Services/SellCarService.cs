using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CarService.BL.Interfaces;
using CarService.Models.Entities;
using CarService.Models.Responses;

namespace CarService.BL.Services
{
    internal class SellCarService : ISellCarService
    {

        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;

        public SellCarService(ICustomerService customerService, ICarService carService)
        {
            _customerService = customerService;
            _carService = carService;
        }

        // Implement the service methods here
        public SellCarResponse SellCar(Guid carId, Guid customerId)
        {
            var car = _carService.GetCarById(carId);
            var customer = _customerService.GetById(customerId);


            if (car == null || customer == null)
            {
                throw new ArgumentException("Invalid car or customer ID.");
            }

            var price = car.BasePrice - customer.Discount;

            return new SellCarResponse
            {
                Customer = customer,
                Car = car,
                SalePrice = price < 0 ? 0 : price
            };
        }
    }
}