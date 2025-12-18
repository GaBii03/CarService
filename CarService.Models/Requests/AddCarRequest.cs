using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models.Requests
{
    public class AddCarRequest
    {
        public string Model { get; set; }

        public int Year { get; set; }

    }
}