using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCustomer = await _customerService.AddCustomerAsync(dto);

            return Ok(new
            {
                message = "Customer created successfully",
                customer = createdCustomer
            });
        }

        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _customerService.UpdateCustomerAsync(id, dto);
            return Ok(new { message = "Customer updated successfully" });
        }

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok(new { message = "Customer deleted successfully" });
        }
    }
}
