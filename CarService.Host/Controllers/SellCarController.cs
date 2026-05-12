using CarService.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellCarController : ControllerBase
    {
        private readonly ISellCarService _sellCarService;
        private readonly ILogger<SellCarController> _logger;

        public SellCarController(ISellCarService sellCarService, ILogger<SellCarController> logger)
        {
            _sellCarService = sellCarService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SellCar([FromQuery] Guid carId, [FromQuery] Guid customerId)
        {
            if (carId == Guid.Empty || customerId == Guid.Empty)
                return BadRequest("Car ID and Customer ID are required.");

            var result = _sellCarService.SellCar(carId, customerId);
            return Ok(result);
        }
    }
}
