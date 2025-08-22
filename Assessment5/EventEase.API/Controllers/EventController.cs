using EventEase.Core.DTOs;
using EventEase.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        // ----------------- SYNC -----------------
        [HttpGet("sync")]
        public ActionResult<IEnumerable<EventResponseDTO>> GetAll()
        {
            var events = _service.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("sync/{id}", Name = "GetEventByIdSync")]
        public ActionResult<EventResponseDTO> GetById(int id)
        {
            var ev = _service.GetEventById(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        [HttpPost("sync")]
        public ActionResult Create([FromBody] EventRequestDTO dto)
        {
            var id = _service.AddEvent(dto);
            var createdEvent = _service.GetEventById(id);
            return CreatedAtRoute("GetEventByIdSync", new { id }, createdEvent);
        }

        [HttpPut("sync/{id}")]
        public ActionResult Update(int id, [FromBody] EventRequestDTO dto)
        {
            var existing = _service.GetEventById(id);
            if (existing == null)
                return NotFound();

            _service.UpdateEvent(id, dto);
            return Ok("Event updated successfully.");
        }

        [HttpDelete("sync/{id}")]
        public ActionResult Delete(int id)
        {
            var existing = _service.GetEventById(id);
            if (existing == null)
                return NotFound();

            _service.DeleteEvent(id);
            return Ok("Event deleted successfully.");
        }

        // ----------------- ASYNC -----------------
        [HttpGet("async")]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetAllAsync()
        {
            var events = await _service.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("async/{id}", Name = "GetEventByIdAsync")]
        public async Task<ActionResult<EventResponseDTO>> GetByIdAsync(int id)
        {
            var ev = await _service.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        [HttpPost("async")]
        public async Task<ActionResult> CreateAsync([FromBody] EventRequestDTO dto)
        {
            var id = await _service.AddEventAsync(dto);
            var createdEvent = await _service.GetEventByIdAsync(id);
            return CreatedAtRoute("GetEventByIdAsync", new { id }, createdEvent);
        }

        [HttpPut("async/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] EventRequestDTO dto)
        {
            var existing = await _service.GetEventByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateEventAsync(id, dto);
            return Ok("Event updated successfully.");
        }

        [HttpDelete("async/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var existing = await _service.GetEventByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteEventAsync(id);
            return Ok("Event deleted successfully.");
        }
    }
}
