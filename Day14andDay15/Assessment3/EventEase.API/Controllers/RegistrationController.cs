using EventEase.Core.DTOs;
using EventEase.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;

            // Sample registrations (assumes userId and eventId exist)
            _registrationService.AddRegistration(new RegistrationRequestDTO { UserId = 1, EventId = 1 });
            _registrationService.AddRegistration(new RegistrationRequestDTO { UserId = 2, EventId = 2 });
        }

        [HttpGet("All")]
        public ActionResult<List<RegistrationResponseDTO>> GetAllRegistrations()
        {
            var registrations = _registrationService.GetAllRegistrations();
            return Ok(registrations);
        }

        [HttpGet("{id}")]
        public ActionResult<RegistrationResponseDTO> GetRegistrationById(int id)
        {
            var registration = _registrationService.GetRegistrationById(id);
            if (registration == null) return NotFound($"Registration with ID {id} not found.");
            return Ok(registration);
        }

        [HttpPost("Add")]
        public IActionResult AddRegistration([FromBody] RegistrationRequestDTO registrationDto)
        {
            _registrationService.AddRegistration(registrationDto);
            return Ok("Registration added successfully.");
        }
    }
}
