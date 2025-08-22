using EventEase.Core.DTOs;
using EventEase.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // ----------------- SYNC -----------------
        [HttpPost("add")]
        public IActionResult Add([FromBody] RegistrationRequestDTO request)
        {
            var id = _registrationService.AddRegistration(request);
            return Ok(new { Id = id });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reg = _registrationService.GetRegistrationById(id);
            if (reg == null) return NotFound();
            return Ok(reg);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var regs = _registrationService.GetAllRegistrations();
            return Ok(regs);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] RegistrationRequestDTO request)
        {
            _registrationService.UpdateRegistration(id, request);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _registrationService.DeleteRegistration(id);
            return NoContent();
        }

        // ----------------- ASYNC -----------------
        [HttpPost("add-async")]
        public async Task<IActionResult> AddAsync([FromBody] RegistrationRequestDTO request)
        {
            var id = await _registrationService.AddRegistrationAsync(request);
            return Ok(new { Id = id });
        }

        [HttpGet("get-async/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var reg = await _registrationService.GetRegistrationByIdAsync(id);
            if (reg == null) return NotFound();
            return Ok(reg);
        }

        [HttpGet("all-async")]
        public async Task<IActionResult> GetAllAsync()
        {
            var regs = await _registrationService.GetAllRegistrationsAsync();
            return Ok(regs);
        }

        [HttpPut("update-async/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] RegistrationRequestDTO request)
        {
            await _registrationService.UpdateRegistrationAsync(id, request);
            return NoContent();
        }

        [HttpDelete("delete-async/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _registrationService.DeleteRegistrationAsync(id);
            return NoContent();
        }
    }
}
