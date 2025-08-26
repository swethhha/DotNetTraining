using BugTrack.Core.DTOs;
using BugTrack.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugTracker.API.Controllers
{
    [ApiExplorerSettings(GroupName = "bugs")]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class BugController : ControllerBase
    {
        private readonly IBugService _service;

        public BugController(IBugService service)
        {
            _service = service;
        }

        // ----------------- SYNC -----------------

        [HttpGet("sync")]
        [Authorize(Roles = "Admin,User,Developer")]
        public ActionResult<IEnumerable<BugResponseDTO>> GetAll()
        {
            var bugs = _service.GetAllBugs();
            return Ok(bugs);
        }

        [HttpGet("sync/{id}", Name = "GetBugByIdSync")]
        [Authorize(Roles = "Admin,User,Developer")]
        public ActionResult<BugResponseDTO> GetById(int id)
        {
            var bug = _service.GetBugById(id);
            if (bug == null)
                return NotFound();

            return Ok(bug);
        }

        [HttpPost("sync")]
        [Authorize(Roles = "Admin,Developer")]
        public ActionResult Create([FromBody] BugResquestDTO dto)
        {
            var id = _service.CreateBug(dto);
            var createdBug = _service.GetBugById(id);
            return CreatedAtRoute("GetBugByIdSync", new { id }, createdBug);
        }

        [HttpPut("sync/{id}")]
        [Authorize(Roles = "Admin,Developer")]
        public ActionResult Update(int id, [FromBody] BugResquestDTO dto)
        {
            var existing = _service.GetBugById(id);
            if (existing == null)
                return NotFound();

            _service.UpdateBug(id, dto);
            return Ok("Bug updated successfully.");
        }

        [HttpDelete("sync/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var existing = _service.GetBugById(id);
            if (existing == null)
                return NotFound();

            _service.DeleteBug(id);
            return Ok("Bug deleted successfully.");
        }

        // ----------------- ASYNC -----------------
        [HttpGet("async")]
        [Authorize(Roles = "Admin,User,Developer")]
        public async Task<ActionResult<IEnumerable<BugResponseDTO>>> GetAllAsync()
        {
            var bugs = await _service.GetAllBugsAsync();
            return Ok(bugs);
        }

        [HttpGet("async/{id}", Name = "GetBugByIdAsync")]
        [Authorize(Roles = "Admin,User,Developer")]
        public async Task<ActionResult<BugResponseDTO>> GetByIdAsync(int id)
        {
            var bug = await _service.GetBugByIdAsync(id);
            if (bug == null)
                return NotFound();

            return Ok(bug);
        }

        [HttpPost("async")]
        [Authorize(Roles = "Admin,Developer")]
        public async Task<ActionResult> CreateAsync([FromBody] BugResquestDTO dto)
        {
            var id = await _service.CreateBugAsync(dto);
            var createdBug = await _service.GetBugByIdAsync(id);
            return CreatedAtRoute("GetBugByIdAsync", new { id }, createdBug);
        }

        [HttpPut("async/{id}")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] BugResquestDTO dto)
        {
            var existing = await _service.GetBugByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateBugAsync(id, dto);
            return Ok("Bug updated successfully.");
        }

        [HttpDelete("async/{id}")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var existing = await _service.GetBugByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteBugAsync(id);
            return Ok("Bug deleted successfully.");
        }
        [HttpGet("me")]
        [Authorize]
        public ActionResult GetMyClaims()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return Ok(new { username, role });
        }
    }
}
