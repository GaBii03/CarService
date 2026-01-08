using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Models.Responses;

namespace CarService.BL.Interfaces
{
    public interface ISellCarService
    {
        SellCarResponse SellCar(Guid carId, Guid customerId);
    }
}