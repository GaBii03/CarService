using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal BasePrice { get; set; }
    }
}