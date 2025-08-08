using BugTrack.Core.DTOs;
using BugTrack.Core.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IActionResult GetAllBugs() => Ok(_service.GetAllBugs());
        [HttpGet("{id}")]
        public IActionResult GetBugById(int id)
        {
            var bug = _service.GetBugById(id);
            if (bug == null) return NotFound();
            return Ok(bug);
        }
        //[HttpPost]
        //public IActionResult AddBug([FromBody] BugResquestDTO bug)
        //{
        //    if (bug == null) return BadRequest("Bug data is required.");
        //    _service.AddBug(bug);
        //    return CreatedAtAction(nameof(GetBugById), new { id = bug.Id }, bug);
        //}
        [HttpPut("{id}")]
        public IActionResult UpdateBug(int id, [FromBody] BugResquestDTO bug)
        {
            if (bug == null) return BadRequest("Bug data is required.");
            _service.UpdateBug(id, bug);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBug(int id)
        {
            _service.DeleteBug(id);
            return NoContent();
        }
        [HttpGet("status/{status}")]
        public IActionResult GetBugsByStatus(string status)
        {
            var bugs = _service.GetAllBugs().Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            if (bugs.Count == 0) return NotFound($"No bugs found with status '{status}'.");
            return Ok(bugs);

        }
        //[HttpGet("project/{projectId}")]
        //public IActionResult GetBugsByProjectId(int projectId)
        //{
        //    var bugs = _service.GetAllBugs().Where(b => b.ProjectId == projectId).ToList();
        //    if (bugs.Count == 0) return NotFound($"No bugs found for project ID '{projectId}'.");
        //    return Ok(bugs);
        //}


    }

}