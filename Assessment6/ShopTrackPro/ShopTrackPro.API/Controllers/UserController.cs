using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Interfaces;
using System.Security.Claims;

namespace ShopTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service) => _service = service;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll()
            => Ok(await _service.GetAllUsersAsync());

        [HttpGet("{id}", Name = "GetUserById")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseDTO>> GetById(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<UserResponseDTO>> Create([FromBody] UserRequestDTO dto)
        {
            var user = await _service.CreateUserAsync(dto);
            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User,Seller")]
        public async Task<ActionResult<UserResponseDTO>> Update(int id, [FromBody] UserRequestDTO dto)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentRole = User.FindFirstValue(ClaimTypes.Role);

            if (currentRole != "Admin" && currentUserId != id)
                return Forbid();

            var updatedUser = await _service.UpdateUserAsync(id, dto);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteUserAsync(id);
            return Ok(new { message = "User deleted successfully." });
        }

        [HttpGet("me")]
        [Authorize(Roles = "Admin,User,Seller")]
        public async Task<ActionResult<UserResponseDTO>> GetMyProfile()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _service.GetUserByIdAsync(currentUserId);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
