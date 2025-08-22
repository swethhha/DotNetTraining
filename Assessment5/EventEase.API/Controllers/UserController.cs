using EventEase.Core.DTOs;
using EventEase.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // ----------------- SYNC -----------------
        [HttpGet("sync")]
        public ActionResult<IEnumerable<UserResponseDTO>> GetAll()
        {
            var users = _service.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("sync/{id}", Name = "GetUserByIdSync")]
        public ActionResult<UserResponseDTO> GetById(int id)
        {
            var user = _service.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("sync")]
        public ActionResult Create([FromBody] UserRequestDTO dto)
        {
            var id = _service.AddUser(dto);
            var createdUser = _service.GetUserById(id);
            return CreatedAtRoute("GetUserByIdSync", new { id }, createdUser);
        }

        [HttpPut("sync/{id}")]
        public ActionResult Update(int id, [FromBody] UserRequestDTO dto)
        {
            var existing = _service.GetUserById(id);
            if (existing == null)
                return NotFound();

            _service.UpdateUser(id, dto);
            return Ok("User updated successfully.");
        }

        [HttpDelete("sync/{id}")]
        public ActionResult Delete(int id)
        {
            var existing = _service.GetUserById(id);
            if (existing == null)
                return NotFound();

            _service.DeleteUser(id);
            return Ok("User deleted successfully.");
        }

        // ----------------- ASYNC -----------------
        [HttpGet("async")]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllAsync()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("async/{id}", Name = "GetUserByIdAsync")]
        public async Task<ActionResult<UserResponseDTO>> GetByIdAsync(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("async")]
        public async Task<ActionResult> CreateAsync([FromBody] UserRequestDTO dto)
        {
            var id = await _service.AddUserAsync(dto);
            var createdUser = await _service.GetUserByIdAsync(id);
            return CreatedAtRoute("GetUserByIdAsync", new { id }, createdUser);
        }

        [HttpPut("async/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UserRequestDTO dto)
        {
            var existing = await _service.GetUserByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateUserAsync(id, dto);
            return Ok("User updated successfully.");
        }

        [HttpDelete("async/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var existing = await _service.GetUserByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteUserAsync(id);
            return Ok("User deleted successfully.");
        }
    }
}
