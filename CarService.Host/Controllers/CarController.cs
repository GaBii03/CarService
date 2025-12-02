using CarService.BL.Interfaces;
using CarService.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Car>> GetAllCars()
        {
            try
            {
                var cars = _carService.GetAllCars();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all cars");
                return StatusCode(500, "An error occurred while retrieving cars");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetCarById(Guid id)
        {
            try
            {
                var car = _carService.GetCarById(id);
                if (car == null)
                {
                    return NotFound($"Car with id {id} not found");
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving car with id {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the car");
            }
        }

        [HttpPost]
        public ActionResult<Car> AddCar([FromBody] Car car)
        {
            try
            {
                if (car == null)
                {
                    return BadRequest("Car data is required");
                }

                if (car.Id == Guid.Empty)
                {
                    car.Id = Guid.NewGuid();
                }

                _carService.AddCar(car);
                return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding car");
                return StatusCode(500, "An error occurred while adding the car");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCar(Guid id, [FromBody] Car car)
        {
            try
            {
                if (car == null)
                {
                    return BadRequest("Car data is required");
                }

                if (id != car.Id)
                {
                    return BadRequest("Car ID mismatch");
                }

                var existingCar = _carService.GetCarById(id);
                if (existingCar == null)
                {
                    return NotFound($"Car with id {id} not found");
                }

                _carService.UpdateCar(car);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating car with id {Id}", id);
                return StatusCode(500, "An error occurred while updating the car");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(Guid id)
        {
            try
            {
                var car = _carService.GetCarById(id);
                if (car == null)
                {
                    return NotFound($"Car with id {id} not found");
                }

                _carService.DeleteCar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car with id {Id}", id);
                return StatusCode(500, "An error occurred while deleting the car");
            }
        }
    }
}

