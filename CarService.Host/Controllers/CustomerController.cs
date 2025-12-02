using CarService.BL.Interfaces;
using CarService.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAllCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all customers");
                return StatusCode(500, "An error occurred while retrieving customers");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            try
            {
                var customer = _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound($"Customer with id {id} not found");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer with id {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the customer");
            }
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is required");
                }

                _customerService.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding customer");
                return StatusCode(500, "An error occurred while adding the customer");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is required");
                }

                if (id != customer.Id)
                {
                    return BadRequest("Customer ID mismatch");
                }

                var existingCustomer = _customerService.GetCustomerById(id);
                if (existingCustomer == null)
                {
                    return NotFound($"Customer with id {id} not found");
                }

                _customerService.UpdateCustomer(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer with id {Id}", id);
                return StatusCode(500, "An error occurred while updating the customer");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var customer = _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound($"Customer with id {id} not found");
                }

                _customerService.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer with id {Id}", id);
                return StatusCode(500, "An error occurred while deleting the customer");
            }
        }
    }
}

