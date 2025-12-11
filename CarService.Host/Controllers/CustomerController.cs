using CarService.BL.Interfaces;
using CarService.Models.Entities;
using CarService.Models.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;

        public CustomerController(
            ICustomerService customerService,
            ILogger<CustomerController> logger,
            IMapper mapper)
        {
            _customerService = customerService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetAllCustomers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllCustomers()
        {
            var customers =
                _customerService.GetAll();

            if (customers?.Count == 0) return NoContent();

            return Ok(customers);
        }

        [HttpGet(nameof(GetCustomerById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCustomerById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id must be greater than zero.");
            }

            var customer =
                _customerService.GetById(id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpPost(nameof(AddCustomer))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer([FromBody] AddCustomerRequest? request)
        {
            if (request == null)
            {
                return BadRequest("Customer cannot be null.");
            }

            var customer = _mapper.Map<Customer>(request);

            if (customer == null) return BadRequest("Mapping failed.");

            _customerService.Add(customer);

            return Ok();
        }

        [HttpDelete(nameof(DeleteCustomer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id must be greater than zero.");
            }

            _customerService.Delete(id);

            return Ok();
        }
    }
}

