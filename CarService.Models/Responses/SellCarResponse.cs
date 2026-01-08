using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Models.Entities;

namespace CarService.Models.Responses
{
    public class SellCarResponse
    {
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public decimal SalePrice { get; set; }

        public decimal Discount { get; set; }
    }
}