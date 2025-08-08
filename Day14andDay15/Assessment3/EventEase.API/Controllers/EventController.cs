using EventEase.Core.DTOs;
using EventEase.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventEase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;

            // Sample events
            _eventService.AddEvent(new EventRequestDTO
            {
                Title = "Tech Conference",
                Location = "Bangalore",
                Description = "Annual technology conference."
            });

            _eventService.AddEvent(new EventRequestDTO
            {
                Title = "Music Fest",
                Location = "Chennai",
                Description = "Live music festival with popular bands."
            });
        }

        [HttpGet("All")]
        public ActionResult<List<EventResponseDTO>> GetAllEvents()
        {
            var events = _eventService.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public ActionResult<EventResponseDTO> GetEventById(int id)
        {
            var ev = _eventService.GetEventById(id);
            if (ev == null) return NotFound($"Event with ID {id} not found.");
            return Ok(ev);
        }

        [HttpPost("Add")]
        public IActionResult AddEvent([FromBody] EventRequestDTO eventDto)
        {
            _eventService.AddEvent(eventDto);
            return Ok("Event added successfully.");
        }
    }
}
