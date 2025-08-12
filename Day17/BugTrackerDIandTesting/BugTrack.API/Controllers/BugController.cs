using BugTrack.Core.DTOs;
using BugTrack.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IBugService _service;

        public BugController(IBugService service)
        {
            _service = service;
        }

        // GET: api/bug
        [HttpGet]
        public IActionResult GetAllBugs()
        {
            var bugs = _service.GetAllBugs();
            if (bugs == null || bugs.Count == 0)
                return NotFound(new { message = "No bugs found." });

            return Ok(bugs);
        }

        // GET: api/bug/{id}
        [HttpGet("{id}")]
        public IActionResult GetBugById(int id)
        {
            var bug = _service.GetBugById(id);
            if (bug == null)
                return NotFound(new { message = $"Bug with ID {id} not found." });

            return Ok(bug);
        }

        // POST: api/bug
        [HttpPost]
        public IActionResult AddBug([FromBody] BugResquestDTO bugDto)
        {
            if (bugDto == null || string.IsNullOrWhiteSpace(bugDto.Title))
                return BadRequest(new { message = "Invalid bug data." });

            _service.AddBug(bugDto);
            return StatusCode(201, new { message = "Bug created successfully." });
        }

        // PUT: api/bug/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBug(int id, [FromBody] BugResquestDTO bugDto)
        {
            if (bugDto == null || string.IsNullOrWhiteSpace(bugDto.Title))
                return BadRequest(new { message = "Invalid bug data." });

            var existingBug = _service.GetBugById(id);
            if (existingBug == null)
                return NotFound(new { message = $"Bug with ID {id} not found." });

            _service.UpdateBug(id, bugDto);
            return NoContent(); // 204 No Content
        }

        // DELETE: api/bug/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBug(int id)
        {
            var existingBug = _service.GetBugById(id);
            if (existingBug == null)
                return NotFound(new { message = $"Bug with ID {id} not found." });

            _service.DeleteBug(id);
            return NoContent(); // 204 No Content
        }
    }
}
